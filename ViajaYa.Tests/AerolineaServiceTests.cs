using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ViajaYa.Data;
using ViajaYa.Models;
using ViajaYa.Services;

namespace ViajaYa.Tests;

public class AerolineaServiceTests : IDisposable
{
    private readonly SqliteConnection _conexion;
    private readonly TestDbContextFactory _factory;
    private readonly AerolineaService _service;

    public AerolineaServiceTests()
    {
        _conexion = new SqliteConnection("Data Source=:memory:");
        _conexion.Open();

        var opciones = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_conexion)
            .Options;

        using var context = new ApplicationDbContext(opciones);
        context.Database.EnsureCreated();

        _factory = new TestDbContextFactory(opciones);
        _service = new AerolineaService(_factory);
    }

    [Fact]
    public async Task GetList_DebeObtenerAerolineasSegunFiltro()
    {
        using (var context = _factory.CreateDbContext())
        {
            context.Aerolineas.AddRange(
                new Aerolinea { Nombre = "Air Europa", Pais = "Espana" },
                new Aerolinea { Nombre = "American Airlines", Pais = "Estados Unidos" }
            );

            await context.SaveChangesAsync();
        }

        var resultado = await _service.GetList(x => x.Nombre == "Air Europa");

        Assert.True(resultado.IsSuccess);
        Assert.Single(resultado.Value);
        Assert.Equal("Air Europa", resultado.Value[0].Nombre);
    }

    [Fact]
    public async Task Buscar_DebeEncontrarAerolineaExistente()
    {
        int id;

        using (var context = _factory.CreateDbContext())
        {
            var aerolinea = new Aerolinea
            {
                Nombre = "Air Europa",
                Pais = "Espana"
            };

            context.Aerolineas.Add(aerolinea);
            await context.SaveChangesAsync();

            id = aerolinea.AerolineaId;
        }

        var resultado = await _service.Buscar(id);

        Assert.True(resultado.IsSuccess);
        Assert.NotNull(resultado.Value);
        Assert.Equal("Air Europa", resultado.Value.Nombre);
    }

    [Fact]
    public async Task Buscar_DebeRetornarNullCuandoNoExiste()
    {
        var resultado = await _service.Buscar(999);

        Assert.True(resultado.IsSuccess);
        Assert.Null(resultado.Value);
    }

    [Fact]
    public async Task Guardar_DebeRegistrarAerolineaNueva()
    {
        var aerolinea = new Aerolinea
        {
            Nombre = "Copa Airlines",
            Pais = "Panama"
        };

        var resultado = await _service.Guardar(aerolinea);

        Assert.True(resultado.IsSuccess);

        using var context = _factory.CreateDbContext();

        var guardada = await context.Aerolineas
            .FirstOrDefaultAsync(x => x.Nombre == "Copa Airlines");

        Assert.NotNull(guardada);
        Assert.Equal("Panama", guardada.Pais);
    }

    [Fact]
    public async Task Guardar_DebeActualizarAerolineaExistente()
    {
        int id;

        using (var context = _factory.CreateDbContext())
        {
            var aerolinea = new Aerolinea
            {
                Nombre = "Copa Airlines",
                Pais = "Panama"
            };

            context.Aerolineas.Add(aerolinea);
            await context.SaveChangesAsync();

            id = aerolinea.AerolineaId;
        }

        var actualizada = new Aerolinea
        {
            AerolineaId = id,
            Nombre = "Copa Airlines RD",
            Pais = "Republica Dominicana"
        };

        var resultado = await _service.Guardar(actualizada);

        Assert.True(resultado.IsSuccess);

        using var nuevoContext = _factory.CreateDbContext();

        var aerolineaGuardada = await nuevoContext.Aerolineas.FindAsync(id);

        Assert.NotNull(aerolineaGuardada);
        Assert.Equal("Copa Airlines RD", aerolineaGuardada.Nombre);
        Assert.Equal("Republica Dominicana", aerolineaGuardada.Pais);
    }

    [Fact]
    public async Task Eliminar_DebeEliminarAerolineaExistente()
    {
        int id;

        using (var context = _factory.CreateDbContext())
        {
            var aerolinea = new Aerolinea
            {
                Nombre = "Copa Airlines",
                Pais = "Panama"
            };

            context.Aerolineas.Add(aerolinea);
            await context.SaveChangesAsync();

            id = aerolinea.AerolineaId;
        }

        var resultado = await _service.Eliminar(id);

        Assert.True(resultado.IsSuccess);

        using var nuevoContext = _factory.CreateDbContext();

        var eliminada = await nuevoContext.Aerolineas.FindAsync(id);

        Assert.Null(eliminada);
    }

    [Fact]
    public async Task Eliminar_DebeRetornarErrorCuandoAerolineaNoExiste()
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
