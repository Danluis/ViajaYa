using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ViajaYa.Data;
using ViajaYa.Models;

namespace ViajaYa.Services;

public class HotelService : IServiceResult<Hotel, int>
{
    private readonly IDbContextFactory<ApplicationDbContext> _factory;

    public HotelService(IDbContextFactory<ApplicationDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<Result<List<Hotel>>> GetList(Expression<Func<Hotel, bool>> filtro)
    {
        using var context = await _factory.CreateDbContextAsync();
        var lista = await context.Hoteles
            .Include(h => h.Destino)
            .Where(filtro)
            .ToListAsync();
        return Result.Success(lista);
    }

    public async Task<Result<Hotel?>> Buscar(int id)
    {
        using var context = await _factory.CreateDbContextAsync();
        var resultado = await context.Hoteles
            .Include(h => h.Destino)
            .FirstOrDefaultAsync(x => x.HotelId == id);
        return Result.Success(resultado);
    }

    public async Task<Result> Guardar(Hotel entidad)
    {
        try
        {
            using var context = await _factory.CreateDbContextAsync();

            entidad.Destino = null;

            if (entidad.HotelId == 0)
                context.Hoteles.Add(entidad);
            else
                context.Hoteles.Update(entidad);

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
            var entidad = await context.Hoteles.FirstOrDefaultAsync(x => x.HotelId == id);
            if (entidad == null)
                return Result.Failure(new Error("NoEncontrado", "El hotel no existe"));
            context.Hoteles.Remove(entidad);
            await context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("ErrorEliminar", ex.Message));
        }
    }
}
