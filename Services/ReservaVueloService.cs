using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ViajaYa.Data;
using ViajaYa.Models;

namespace ViajaYa.Services;

public class ReservaVueloService : IServiceResult<ReservaVuelo, int>
{
    private readonly IDbContextFactory<ApplicationDbContext> _factory;

    private const string EstadoCancelada = "Cancelada";

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
            .Include(r => r.Detalles)
                .ThenInclude(d => d.Vuelo)
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
            .Include(r => r.Detalles)
                .ThenInclude(d => d.Vuelo)
            .FirstOrDefaultAsync(r => r.ReservaVueloId == id);

        return Result.Success(resultado);
    }

    public async Task<Result> Guardar(ReservaVuelo entidad)
    {
        try
        {
            using var context = await _factory.CreateDbContextAsync();

            bool nuevaReserva = entidad.ReservaVueloId == 0;
            bool nuevaCancelada = EsCancelada(entidad.Estado);

            if (nuevaReserva)
            {
                if (!nuevaCancelada)
                {
                    var resultadoValidacion = await DescontarAsientos(
                        context, entidad.Detalles);

                    if (resultadoValidacion.HasValue)
                    {
                        return Result.Failure(resultadoValidacion.Value);
                    }
                }

                entidad.Cliente = null;
                entidad.Agente = null;

                foreach (var detalle in entidad.Detalles)
                {
                    detalle.Vuelo = null;
                }

                context.ReservaVuelos.Add(entidad);
            }
            else
            {
                var reservaExistente = await context.ReservaVuelos
                    .Include(r => r.Detalles)
                    .FirstOrDefaultAsync(r => r.ReservaVueloId == entidad.ReservaVueloId);

                if (reservaExistente == null)
                {
                    return Result.Failure(
                        new Error("NoEncontrado", "La reserva no existe"));
                }

                bool anteriorCancelada = EsCancelada(reservaExistente.Estado);

                // Devolver los asientos de la reserva anterior si estaba activa.
                if (!anteriorCancelada)
                {
                    await DevolverAsientos(context, reservaExistente.Detalles);
                }

                // Descontar los asientos nuevos si la reserva no está cancelada.
                if (!nuevaCancelada)
                {
                    var resultadoValidacion = await DescontarAsientos(
                        context, entidad.Detalles);

                    if (resultadoValidacion.HasValue)
                    {
                        return Result.Failure(resultadoValidacion.Value);
                    }
                }

                foreach (var detalle in entidad.Detalles)
                {
                    detalle.Vuelo = null;
                    detalle.ReservaVuelo = null;
                    detalle.DetalleId = 0;
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
                .FirstOrDefaultAsync(r => r.ReservaVueloId == id);

            if (entidad == null)
            {
                return Result.Failure(
                    new Error("NoEncontrado", "La reserva no existe"));
            }

            // Si ya estaba cancelada, sus asientos fueron devueltos.
            if (!EsCancelada(entidad.Estado))
            {
                await DevolverAsientos(context, entidad.Detalles);
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

    private static bool EsCancelada(string? estado)
    {
        return string.Equals(
            estado?.Trim(),
            EstadoCancelada,
            StringComparison.OrdinalIgnoreCase);
    }

    private static async Task<Error?> DescontarAsientos(
        ApplicationDbContext context,
        List<DetalleReservaVuelo> detalles)
    {
        foreach (var detalle in detalles)
        {
            if (detalle.CantidadAsientos <= 0)
            {
                return new Error(
                    "CantidadInvalida",
                    "La cantidad de asientos debe ser mayor que cero");
            }

            var vuelo = await context.Vuelos
                .FirstOrDefaultAsync(v => v.VueloId == detalle.VueloId);

            if (vuelo == null)
            {
                return new Error(
                    "VueloNoEncontrado",
                    "Uno de los vuelos seleccionados no existe");
            }

            if (vuelo.AsientosDisponibles < detalle.CantidadAsientos)
            {
                return new Error(
                    "SinDisponibilidad",
                    $"El vuelo {vuelo.NumeroVuelo} no tiene suficientes asientos disponibles");
            }

            vuelo.AsientosDisponibles -= detalle.CantidadAsientos;
        }

        return null;
    }

    private static async Task DevolverAsientos(
        ApplicationDbContext context,
        List<DetalleReservaVuelo> detalles)
    {
        foreach (var detalle in detalles)
        {
            var vuelo = await context.Vuelos
                .FirstOrDefaultAsync(v => v.VueloId == detalle.VueloId);

            if (vuelo != null)
            {
                vuelo.AsientosDisponibles += detalle.CantidadAsientos;
            }
        }
    }
}
