using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ViajaYa.Data;
using ViajaYa.Models;
using ViajaYa.Services;

namespace ViajaYa.Tests;

public class AgenteServiceTests : IDisposable
{
    private readonly SqliteConnection _conexion;
    private readonly TestDbContextFactory _factory;
    private readonly AgenteService _service;

    public AgenteServiceTests()
    {
        _conexion = new SqliteConnection("Data Source=:memory:");
        _conexion.Open();

        var opciones = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_conexion)
            .Options;

        using var context = new ApplicationDbContext(opciones);
        context.Database.EnsureCreated();

        _factory = new TestDbContextFactory(opciones);
        _service = new AgenteService(_factory);
    }

    [Fact]
    public async Task GetList_DebeObtenerAgentesSegunFiltro()
    {
        using (var context = _factory.CreateDbContext())
        {
            context.Agentes.AddRange(
                new Agente { Nombre = "Carlos Perez", Codigo = "AG001" },
                new Agente { Nombre = "Maria Lopez", Codigo = "AG002" }
            );

            await context.SaveChangesAsync();
        }

        var resultado = await _service.GetList(x => x.Nombre.Contains("Carlos"));

        Assert.True(resultado.IsSuccess);
        Assert.Single(resultado.Value);
        Assert.Equal("Carlos Perez", resultado.Value[0].Nombre);
    }

    [Fact]
    public async Task Buscar_DebeEncontrarAgenteExistente()
    {
        int id;

        using (var context = _factory.CreateDbContext())
        {
            var agente = new Agente
            {
                Nombre = "Carlos Perez",
                Codigo = "AG001"
            };

            context.Agentes.Add(agente);
            await context.SaveChangesAsync();

            id = agente.AgenteId;
        }

        var resultado = await _service.Buscar(id);

        Assert.True(resultado.IsSuccess);
        Assert.NotNull(resultado.Value);
        Assert.Equal("Carlos Perez", resultado.Value.Nombre);
    }

    [Fact]
    public async Task Buscar_DebeRetornarNullCuandoNoExiste()
    {
        var resultado = await _service.Buscar(999);

        Assert.True(resultado.IsSuccess);
        Assert.Null(resultado.Value);
    }

    [Fact]
    public async Task Guardar_DebeRegistrarAgenteNuevo()
    {
        var agente = new Agente
        {
            Nombre = "Pedro Gomez",
            Codigo = "AG003"
        };

        var resultado = await _service.Guardar(agente);

        Assert.True(resultado.IsSuccess);

        using var context = _factory.CreateDbContext();

        var guardado = await context.Agentes
            .FirstOrDefaultAsync(x => x.Codigo == "AG003");

        Assert.NotNull(guardado);
        Assert.Equal("Pedro Gomez", guardado.Nombre);
    }

    [Fact]
    public async Task Guardar_DebeActualizarAgenteExistente()
    {
        int id;

        using (var context = _factory.CreateDbContext())
        {
            var agente = new Agente
            {
                Nombre = "Pedro Gomez",
                Codigo = "AG003"
            };

            context.Agentes.Add(agente);
            await context.SaveChangesAsync();

            id = agente.AgenteId;
        }

        var actualizado = new Agente
        {
            AgenteId = id,
            Nombre = "Pedro Actualizado",
            Codigo = "AG004"
        };

        var resultado = await _service.Guardar(actualizado);

        Assert.True(resultado.IsSuccess);

        using var nuevoContext = _factory.CreateDbContext();

        var agenteGuardado = await nuevoContext.Agentes.FindAsync(id);

        Assert.NotNull(agenteGuardado);
        Assert.Equal("Pedro Actualizado", agenteGuardado.Nombre);
        Assert.Equal("AG004", agenteGuardado.Codigo);
    }

    [Fact]
    public async Task Eliminar_DebeEliminarAgenteExistente()
    {
        int id;

        using (var context = _factory.CreateDbContext())
        {
            var agente = new Agente
            {
                Nombre = "Pedro Gomez",
                Codigo = "AG003"
            };

            context.Agentes.Add(agente);
            await context.SaveChangesAsync();

            id = agente.AgenteId;
        }

        var resultado = await _service.Eliminar(id);

        Assert.True(resultado.IsSuccess);

        using var nuevoContext = _factory.CreateDbContext();

        var eliminado = await nuevoContext.Agentes.FindAsync(id);

        Assert.Null(eliminado);
    }

    [Fact]
    public async Task Eliminar_DebeRetornarErrorCuandoAgenteNoExiste()
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
