using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ViajaYa.Data;
using ViajaYa.Models;

namespace ViajaYa.Services;

public class AgenteService : IServiceResult<Agente, int>
{
    private readonly IDbContextFactory<ApplicationDbContext> _factory;

    public AgenteService(IDbContextFactory<ApplicationDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<Result<List<Agente>>> GetList(Expression<Func<Agente, bool>> filtro)
    {
        using var context = await _factory.CreateDbContextAsync();
        var lista = await context.Agentes.Where(filtro).ToListAsync();
        return Result.Success(lista);
    }

    public async Task<Result<Agente?>> Buscar(int id)
    {
        using var context = await _factory.CreateDbContextAsync();
        var resultado = await context.Agentes.FirstOrDefaultAsync(x => x.AgenteId == id);
        return Result.Success(resultado);
    }

    public async Task<Result> Guardar(Agente entidad)
    {
        try
        {
            using var context = await _factory.CreateDbContextAsync();
            if (entidad.AgenteId == 0)
                context.Agentes.Add(entidad);
            else
                context.Agentes.Update(entidad);
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
            var entidad = await context.Agentes.FirstOrDefaultAsync(x => x.AgenteId == id);
            if (entidad == null)
                return Result.Failure(new Error("NoEncontrado", "El agente no existe"));
            context.Agentes.Remove(entidad);
            await context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("ErrorEliminar", ex.Message));
        }
    }
}
