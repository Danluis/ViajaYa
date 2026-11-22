using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ViajaYa.Data;
using ViajaYa.Models;

namespace ViajaYa.Services;

public class ReservaVueloService : IServiceResult<ReservaVuelo, int>
{
    private readonly IDbContextFactory<ApplicationDbContext> _factory;

    public ReservaVueloService(IDbContextFactory<ApplicationDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<Result<List<ReservaVuelo>>> GetList(Expression<Func<ReservaVuelo, bool>> filtro)
    {
        using var context = await _factory.CreateDbContextAsync();
        var lista = await context.ReservaVuelos
            .Include(r => r.Cliente)
            .Include(r => r.Agente)
            .Include(r => r.Detalles).ThenInclude(d => d.Vuelo)
            .Where(filtro)
            .ToListAsync();
        return Result.Success(lista);
    }

    public async Task<Result<ReservaVuelo?>> Buscar(int id)
    {
        using var context = await _factory.CreateDbContextAsync();
        var resultado = await context.ReservaVuelos
            .Include(r => r.Cliente)
            .Include(r => r.Agente)
            .Include(r => r.Detalles).ThenInclude(d => d.Vuelo)
            .FirstOrDefaultAsync(x => x.ReservaVueloId == id);
        return Result.Success(resultado);
    }

    public async Task<Result> Guardar(ReservaVuelo entidad)
    {
        try
        {
            using var context = await _factory.CreateDbContextAsync();

            if (entidad.ReservaVueloId == 0)
            {
                // ---- Nueva reserva: validar y descontar asientos ----
                foreach (var detalle in entidad.Detalles)
                {
                    var vuelo = await context.Vuelos.FirstOrDefaultAsync(v => v.VueloId == detalle.VueloId);
                    if (vuelo == null)
                        return Result.Failure(new Error("VueloNoEncontrado", "Uno de los vuelos seleccionados no existe"));

                    if (vuelo.AsientosDisponibles < detalle.CantidadAsientos)
                        return Result.Failure(new Error("SinDisponibilidad", $"El vuelo {vuelo.NumeroVuelo} no tiene suficientes asientos disponibles"));

                    vuelo.AsientosDisponibles -= detalle.CantidadAsientos;
                    detalle.Vuelo = null;
                }

                entidad.Cliente = null;
                entidad.Agente = null;
                context.ReservaVuelos.Add(entidad);
            }
            else
            {
                // ---- Edicion: revertir asientos de los detalles anteriores ----
                var reservaExistente = await context.ReservaVuelos
                    .Include(r => r.Detalles)
                    .FirstOrDefaultAsync(r => r.ReservaVueloId == entidad.ReservaVueloId);

                if (reservaExistente == null)
                    return Result.Failure(new Error("NoEncontrado", "La reserva no existe"));

                foreach (var detalleViejo in reservaExistente.Detalles)
                {
                    var vueloViejo = await context.Vuelos.FirstOrDefaultAsync(v => v.VueloId == detalleViejo.VueloId);
                    if (vueloViejo != null)
                        vueloViejo.AsientosDisponibles += detalleViejo.CantidadAsientos;
                }

                // Validar y descontar los asientos de los detalles nuevos
                foreach (var detalleNuevo in entidad.Detalles)
                {
                    var vuelo = await context.Vuelos.FirstOrDefaultAsync(v => v.VueloId == detalleNuevo.VueloId);
                    if (vuelo == null)
                        return Result.Failure(new Error("VueloNoEncontrado", "Uno de los vuelos seleccionados no existe"));

                    if (vuelo.AsientosDisponibles < detalleNuevo.CantidadAsientos)
                        return Result.Failure(new Error("SinDisponibilidad", $"El vuelo {vuelo.NumeroVuelo} no tiene suficientes asientos disponibles"));

                    vuelo.AsientosDisponibles -= detalleNuevo.CantidadAsientos;

                    detalleNuevo.Vuelo = null;
                    detalleNuevo.ReservaVuelo = null;
                    detalleNuevo.DetalleId = 0;
                }

                context.DetalleReservaVuelos.RemoveRange(reservaExistente.Detalles);

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
            var entidad = await context.ReservaVuelos
                .Include(r => r.Detalles)
                .FirstOrDefaultAsync(x => x.ReservaVueloId == id);

            if (entidad == null)
                return Result.Failure(new Error("NoEncontrado", "La reserva no existe"));

            // Restaurar asientos antes de eliminar
            foreach (var detalle in entidad.Detalles)
            {
                var vuelo = await context.Vuelos.FirstOrDefaultAsync(v => v.VueloId == detalle.VueloId);
                if (vuelo != null)
                    vuelo.AsientosDisponibles += detalle.CantidadAsientos;
            }

            context.ReservaVuelos.Remove(entidad);
            await context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("ErrorEliminar", ex.Message));
        }
    }
}
