using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ViajaYa.Data;
using ViajaYa.Models;

namespace ViajaYa.Services;

public class ClienteService : IServiceResult<Cliente, int>
{
    private readonly IDbContextFactory<ApplicationDbContext> _factory;

    public ClienteService(IDbContextFactory<ApplicationDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<Result<List<Cliente>>> GetList(Expression<Func<Cliente, bool>> filtro)
    {
        using var context = await _factory.CreateDbContextAsync();
        var lista = await context.Clientes.Where(filtro).ToListAsync();
        return Result.Success(lista);
    }

    public async Task<Result<Cliente?>> Buscar(int id)
    {
        using var context = await _factory.CreateDbContextAsync();
        var resultado = await context.Clientes.FirstOrDefaultAsync(x => x.ClienteId == id);
        return Result.Success(resultado);
    }

    public async Task<Result> Guardar(Cliente entidad)
    {
        try
        {
            using var context = await _factory.CreateDbContextAsync();
            if (entidad.ClienteId == 0)
                context.Clientes.Add(entidad);
            else
                context.Clientes.Update(entidad);
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
            var entidad = await context.Clientes.FirstOrDefaultAsync(x => x.ClienteId == id);
            if (entidad == null)
                return Result.Failure(new Error("NoEncontrado", "El cliente no existe"));
            context.Clientes.Remove(entidad);
            await context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("ErrorEliminar", ex.Message));
        }
    }
}
