using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ViajaYa.Data;
using ViajaYa.Models;

namespace ViajaYa.Services;

public class AerolineaService : IServiceResult<Aerolinea, int>
{
    private readonly IDbContextFactory<ApplicationDbContext> _factory;

    public AerolineaService(IDbContextFactory<ApplicationDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<Result<List<Aerolinea>>> GetList(Expression<Func<Aerolinea, bool>> filtro)
    {
        using var context = await _factory.CreateDbContextAsync();
        var lista = await context.Aerolineas.Where(filtro).ToListAsync();
        return Result.Success(lista);
    }

    public async Task<Result<Aerolinea?>> Buscar(int id)
    {
        using var context = await _factory.CreateDbContextAsync();
        var resultado = await context.Aerolineas.FirstOrDefaultAsync(x => x.AerolineaId == id);
        return Result.Success(resultado);
    }

    public async Task<Result> Guardar(Aerolinea entidad)
    {
        try
        {
            using var context = await _factory.CreateDbContextAsync();
            if (entidad.AerolineaId == 0)
                context.Aerolineas.Add(entidad);
            else
                context.Aerolineas.Update(entidad);
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
            var entidad = await context.Aerolineas.FirstOrDefaultAsync(x => x.AerolineaId == id);
            if (entidad == null)
                return Result.Failure(new Error("NoEncontrado", "La aerolinea no existe"));
            context.Aerolineas.Remove(entidad);
            await context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("ErrorEliminar", ex.Message));
        }
    }
}
