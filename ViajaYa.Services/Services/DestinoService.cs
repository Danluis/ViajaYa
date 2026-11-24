using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ViajaYa.Data;
using ViajaYa.Models;

namespace ViajaYa.Services;

public class DestinoService : IServiceResult<Destino, int>
{
    private readonly IDbContextFactory<ApplicationDbContext> _factory;

    public DestinoService(IDbContextFactory<ApplicationDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<Result<List<Destino>>> GetList(Expression<Func<Destino, bool>> filtro)
    {
        using var context = await _factory.CreateDbContextAsync();
        var lista = await context.Destinos.Where(filtro).ToListAsync();
        return Result.Success(lista);
    }

    public async Task<Result<Destino?>> Buscar(int id)
    {
        using var context = await _factory.CreateDbContextAsync();
        var resultado = await context.Destinos.FirstOrDefaultAsync(x => x.DestinoId == id);
        return Result.Success(resultado);
    }

    public async Task<Result> Guardar(Destino entidad)
    {
        try
        {
            using var context = await _factory.CreateDbContextAsync();
            if (entidad.DestinoId == 0)
                context.Destinos.Add(entidad);
            else
                context.Destinos.Update(entidad);
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
            var entidad = await context.Destinos.FirstOrDefaultAsync(x => x.DestinoId == id);
            if (entidad == null)
                return Result.Failure(new Error("NoEncontrado", "El destino no existe"));
            context.Destinos.Remove(entidad);
            await context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("ErrorEliminar", ex.Message));
        }
    }
}
