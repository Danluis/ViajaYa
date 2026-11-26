using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ViajaYa.Data;
using ViajaYa.Models;
using ViajaYa.Services;

namespace ViajaYa.Tests;

public class DestinoServiceTests : IDisposable
{
    private readonly SqliteConnection _conexion;
    private readonly TestDbContextFactory _factory;
    private readonly DestinoService _service;

    public DestinoServiceTests()
    {
        _conexion = new SqliteConnection("Data Source=:memory:");
        _conexion.Open();

        var opciones = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_conexion)
            .Options;

        using var context = new ApplicationDbContext(opciones);
        context.Database.EnsureCreated();

        _factory = new TestDbContextFactory(opciones);
        _service = new DestinoService(_factory);
    }

    [Fact]
    public async Task GetList_DebeObtenerDestinosSegunFiltro()
    {
        using (var context = _factory.CreateDbContext())
        {
            context.Destinos.AddRange(
                new Destino
                {
                    Ciudad = "Madrid",
                    Pais = "Espana",
                    Descripcion = "Capital de Espana"
                },
                new Destino
                {
                    Ciudad = "Paris",
                    Pais = "Francia",
                    Descripcion = "Ciudad de Francia"
                }
            );

            await context.SaveChangesAsync();
        }

        var resultado = await _service.GetList(x => x.Ciudad == "Madrid");

        Assert.True(resultado.IsSuccess);
        Assert.Single(resultado.Value);
        Assert.Equal("Madrid", resultado.Value[0].Ciudad);
    }

    [Fact]
    public async Task Buscar_DebeEncontrarDestinoExistente()
    {
        int id;

        using (var context = _factory.CreateDbContext())
        {
            var destino = new Destino
            {
                Ciudad = "Madrid",
                Pais = "Espana",
                Descripcion = "Capital de Espana"
            };

            context.Destinos.Add(destino);
            await context.SaveChangesAsync();

            id = destino.DestinoId;
        }

        var resultado = await _service.Buscar(id);

        Assert.True(resultado.IsSuccess);
        Assert.NotNull(resultado.Value);
        Assert.Equal("Madrid", resultado.Value.Ciudad);
        Assert.Equal("Espana", resultado.Value.Pais);
    }

    [Fact]
    public async Task Buscar_DebeRetornarNullCuandoNoExiste()
    {
        var resultado = await _service.Buscar(999);

        Assert.True(resultado.IsSuccess);
        Assert.Null(resultado.Value);
    }

    [Fact]
    public async Task Guardar_DebeRegistrarDestinoNuevo()
    {
        var destino = new Destino
        {
            Ciudad = "Punta Cana",
            Pais = "Republica Dominicana",
            Descripcion = "Destino turistico"
        };

        var resultado = await _service.Guardar(destino);

        Assert.True(resultado.IsSuccess);

        using var context = _factory.CreateDbContext();

        var guardado = await context.Destinos
            .FirstOrDefaultAsync(x => x.Ciudad == "Punta Cana");

        Assert.NotNull(guardado);
        Assert.Equal("Republica Dominicana", guardado.Pais);
        Assert.Equal("Destino turistico", guardado.Descripcion);
    }

    [Fact]
    public async Task Guardar_DebeActualizarDestinoExistente()
    {
        int id;

        using (var context = _factory.CreateDbContext())
        {
            var destino = new Destino
            {
                Ciudad = "Punta Cana",
                Pais = "Republica Dominicana",
                Descripcion = "Destino turistico"
            };

            context.Destinos.Add(destino);
            await context.SaveChangesAsync();

            id = destino.DestinoId;
        }

        var actualizado = new Destino
        {
            DestinoId = id,
            Ciudad = "Santo Domingo",
            Pais = "Republica Dominicana",
            Descripcion = "Capital dominicana"
        };

        var resultado = await _service.Guardar(actualizado);

        Assert.True(resultado.IsSuccess);

        using var nuevoContext = _factory.CreateDbContext();

        var destinoGuardado = await nuevoContext.Destinos.FindAsync(id);

        Assert.NotNull(destinoGuardado);
        Assert.Equal("Santo Domingo", destinoGuardado.Ciudad);
        Assert.Equal("Capital dominicana", destinoGuardado.Descripcion);
    }

    [Fact]
    public async Task Eliminar_DebeEliminarDestinoExistente()
    {
        int id;

        using (var context = _factory.CreateDbContext())
        {
            var destino = new Destino
            {
                Ciudad = "Madrid",
                Pais = "Espana",
                Descripcion = "Capital de Espana"
            };

            context.Destinos.Add(destino);
            await context.SaveChangesAsync();

            id = destino.DestinoId;
        }

        var resultado = await _service.Eliminar(id);

        Assert.True(resultado.IsSuccess);

        using var nuevoContext = _factory.CreateDbContext();

        var eliminado = await nuevoContext.Destinos.FindAsync(id);

        Assert.Null(eliminado);
    }

    [Fact]
    public async Task Eliminar_DebeRetornarErrorCuandoDestinoNoExiste()
    {
        var resultado = await _service.Eliminar(999);

        Assert.False(resultado.IsSuccess);
    }

    public void Dispose()
    {
        _conexion.Dispose();
    }

    private class TestDbContextFactory : IDbContextFactory<ApplicationDbContext>
    {
        private readonly DbContextOptions<ApplicationDbContext> _opciones;

        public TestDbContextFactory(DbContextOptions<ApplicationDbContext> opciones)
        {
            _opciones = opciones;
        }

        public ApplicationDbContext CreateDbContext()
        {
            return new ApplicationDbContext(_opciones);
        }

        public Task<ApplicationDbContext> CreateDbContextAsync(
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(CreateDbContext());
        }
    }
}
