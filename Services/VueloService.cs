using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ViajaYa.Data;
using ViajaYa.Models;

namespace ViajaYa.Services;

public class VueloService : IServiceResult<Vuelo, int>
{
    private readonly IDbContextFactory<ApplicationDbContext> _factory;

    public VueloService(IDbContextFactory<ApplicationDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<Result<List<Vuelo>>> GetList(Expression<Func<Vuelo, bool>> filtro)
    {
        using var context = await _factory.CreateDbContextAsync();
        var lista = await context.Vuelos
            .Include(v => v.Aerolinea)
            .Include(v => v.Destino)
            .Where(filtro)
            .ToListAsync();
        return Result.Success(lista);
    }

    public async Task<Result<Vuelo?>> Buscar(int id)
    {
        using var context = await _factory.CreateDbContextAsync();
        var resultado = await context.Vuelos
            .Include(v => v.Aerolinea)
            .Include(v => v.Destino)
            .FirstOrDefaultAsync(x => x.VueloId == id);
        return Result.Success(resultado);
    }

    public async Task<Result> Guardar(Vuelo entidad)
    {
        try
        {
            using var context = await _factory.CreateDbContextAsync();

            entidad.Aerolinea = null;
            entidad.Destino = null;

            if (entidad.VueloId == 0)
                context.Vuelos.Add(entidad);
            else
                context.Vuelos.Update(entidad);

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
            var entidad = await context.Vuelos.FirstOrDefaultAsync(x => x.VueloId == id);
            if (entidad == null)
                return Result.Failure(new Error("NoEncontrado", "El vuelo no existe"));
            context.Vuelos.Remove(entidad);
            await context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("ErrorEliminar", ex.Message));
        }
    }
}
