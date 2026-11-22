using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ViajaYa.Data;
using ViajaYa.Models;

namespace ViajaYa.Services;

public class ReservaHotelService : IServiceResult<ReservaHotel, int>
{
    private readonly IDbContextFactory<ApplicationDbContext> _factory;

    public ReservaHotelService(IDbContextFactory<ApplicationDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<Result<List<ReservaHotel>>> GetList(Expression<Func<ReservaHotel, bool>> filtro)
    {
        using var context = await _factory.CreateDbContextAsync();
        var lista = await context.ReservaHoteles
            .Include(r => r.Cliente)
            .Include(r => r.Agente)
            .Include(r => r.Detalles).ThenInclude(d => d.Hotel)
            .Where(filtro)
            .ToListAsync();
        return Result.Success(lista);
    }

    public async Task<Result<ReservaHotel?>> Buscar(int id)
    {
        using var context = await _factory.CreateDbContextAsync();
        var resultado = await context.ReservaHoteles
            .Include(r => r.Cliente)
            .Include(r => r.Agente)
            .Include(r => r.Detalles).ThenInclude(d => d.Hotel)
            .FirstOrDefaultAsync(x => x.ReservaHotelId == id);
        return Result.Success(resultado);
    }

    public async Task<Result> Guardar(ReservaHotel entidad)
    {
        try
        {
            using var context = await _factory.CreateDbContextAsync();

            if (entidad.ReservaHotelId == 0)
            {
                // ---- Nueva reserva: validar y descontar habitaciones ----
                foreach (var detalle in entidad.Detalles)
                {
                    var hotel = await context.Hoteles.FirstOrDefaultAsync(h => h.HotelId == detalle.HotelId);
                    if (hotel == null)
                        return Result.Failure(new Error("HotelNoEncontrado", "Uno de los hoteles seleccionados no existe"));

                    if (hotel.HabitacionesDisponibles < detalle.CantidadHabitaciones)
                        return Result.Failure(new Error("SinDisponibilidad", $"El hotel {hotel.Nombre} no tiene suficientes habitaciones disponibles"));

                    hotel.HabitacionesDisponibles -= detalle.CantidadHabitaciones;
                    detalle.Hotel = null;
                }

                entidad.Cliente = null;
                entidad.Agente = null;
                context.ReservaHoteles.Add(entidad);
            }
            else
            {
                // ---- Edicion: revertir habitaciones de los detalles anteriores ----
                var reservaExistente = await context.ReservaHoteles
                    .Include(r => r.Detalles)
                    .FirstOrDefaultAsync(r => r.ReservaHotelId == entidad.ReservaHotelId);

                if (reservaExistente == null)
                    return Result.Failure(new Error("NoEncontrado", "La reserva no existe"));

                foreach (var detalleViejo in reservaExistente.Detalles)
                {
                    var hotelViejo = await context.Hoteles.FirstOrDefaultAsync(h => h.HotelId == detalleViejo.HotelId);
                    if (hotelViejo != null)
                        hotelViejo.HabitacionesDisponibles += detalleViejo.CantidadHabitaciones;
                }

                // Validar y descontar las habitaciones de los detalles nuevos
                foreach (var detalleNuevo in entidad.Detalles)
                {
                    var hotel = await context.Hoteles.FirstOrDefaultAsync(h => h.HotelId == detalleNuevo.HotelId);
                    if (hotel == null)
                        return Result.Failure(new Error("HotelNoEncontrado", "Uno de los hoteles seleccionados no existe"));

                    if (hotel.HabitacionesDisponibles < detalleNuevo.CantidadHabitaciones)
                        return Result.Failure(new Error("SinDisponibilidad", $"El hotel {hotel.Nombre} no tiene suficientes habitaciones disponibles"));

                    hotel.HabitacionesDisponibles -= detalleNuevo.CantidadHabitaciones;

                    detalleNuevo.Hotel = null;
                    detalleNuevo.ReservaHotel = null;
                    detalleNuevo.DetalleId = 0;
                }

                context.DetalleReservaHoteles.RemoveRange(reservaExistente.Detalles);

                reservaExistente.ClienteId = entidad.ClienteId;
                reservaExistente.AgenteId = entidad.AgenteId;
                reservaExistente.FechaReserva = entidad.FechaReserva;
                reservaExistente.Estado = entidad.Estado;
                reservaExistente.Detalles = entidad.Detalles;
            }

            await context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("ErrorGuardar", ex.Message));
        }
    }

    public async Task<Result> Eliminar(int id)
    {
        try
        {
            using var context = await _factory.CreateDbContextAsync();
            var entidad = await context.ReservaHoteles
                .Include(r => r.Detalles)
                .FirstOrDefaultAsync(x => x.ReservaHotelId == id);

            if (entidad == null)
                return Result.Failure(new Error("NoEncontrado", "La reserva no existe"));

            // Restaurar habitaciones antes de eliminar
            foreach (var detalle in entidad.Detalles)
            {
                var hotel = await context.Hoteles.FirstOrDefaultAsync(h => h.HotelId == detalle.HotelId);
                if (hotel != null)
                    hotel.HabitacionesDisponibles += detalle.CantidadHabitaciones;
            }

            context.ReservaHoteles.Remove(entidad);
            await context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("ErrorEliminar", ex.Message));
        }
    }
}
