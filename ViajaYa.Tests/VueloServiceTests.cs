using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ViajaYa.Data;
using ViajaYa.Models;
using ViajaYa.Services;

namespace ViajaYa.Tests;

public class VueloServiceTests : IDisposable
{
    private readonly SqliteConnection _conexion;
    private readonly TestDbContextFactory _factory;
    private readonly VueloService _service;

    public VueloServiceTests()
    {
        _conexion = new SqliteConnection("Data Source=:memory:");
        _conexion.Open();

        var opciones = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_conexion)
            .Options;

        using var context = new ApplicationDbContext(opciones);
        context.Database.EnsureCreated();

        _factory = new TestDbContextFactory(opciones);
        _service = new VueloService(_factory);
    }

    [Fact]
    public async Task GetList_DebeObtenerVuelosSegunFiltro()
    {
        int aerolineaId;
        int destinoId;

        using (var context = _factory.CreateDbContext())
        {
            var aerolinea = new Aerolinea
            {
                Nombre = "Air Europa",
                Pais = "Espana"
            };

            var destino = new Destino
            {
                Ciudad = "Madrid",
                Pais = "Espana"
            };

            context.Aerolineas.Add(aerolinea);
            context.Destinos.Add(destino);
            await context.SaveChangesAsync();

            aerolineaId = aerolinea.AerolineaId;
            destinoId = destino.DestinoId;

            context.Vuelos.AddRange(
                CrearVuelo("UX001", aerolineaId, destinoId),
                CrearVuelo("UX002", aerolineaId, destinoId)
            );

            await context.SaveChangesAsync();
        }

        var resultado = await _service.GetList(x => x.NumeroVuelo == "UX001");

        Assert.True(resultado.IsSuccess);
        Assert.Single(resultado.Value);
        Assert.Equal("UX001", resultado.Value[0].NumeroVuelo);

        Assert.NotNull(resultado.Value[0].Aerolinea);
        Assert.Equal("Air Europa", resultado.Value[0].Aerolinea!.Nombre);

        Assert.NotNull(resultado.Value[0].Destino);
        Assert.Equal("Madrid", resultado.Value[0].Destino!.Ciudad);
    }

    [Fact]
    public async Task Buscar_DebeEncontrarVueloExistente()
    {
        int id;
        int aerolineaId;
        int destinoId;

        using (var context = _factory.CreateDbContext())
        {
            var aerolinea = new Aerolinea
            {
                Nombre = "Copa Airlines",
                Pais = "Panama"
            };

            var destino = new Destino
            {
                Ciudad = "Santo Domingo",
                Pais = "Republica Dominicana"
            };

            context.Aerolineas.Add(aerolinea);
            context.Destinos.Add(destino);
            await context.SaveChangesAsync();

            aerolineaId = aerolinea.AerolineaId;
            destinoId = destino.DestinoId;

            var vuelo = CrearVuelo("CM100", aerolineaId, destinoId);

            context.Vuelos.Add(vuelo);
            await context.SaveChangesAsync();

            id = vuelo.VueloId;
        }

        var resultado = await _service.Buscar(id);

        Assert.True(resultado.IsSuccess);
        Assert.NotNull(resultado.Value);
        Assert.Equal("CM100", resultado.Value.NumeroVuelo);

        Assert.NotNull(resultado.Value.Aerolinea);
        Assert.Equal("Copa Airlines", resultado.Value.Aerolinea!.Nombre);

        Assert.NotNull(resultado.Value.Destino);
        Assert.Equal("Santo Domingo", resultado.Value.Destino!.Ciudad);
    }

    [Fact]
    public async Task Buscar_DebeRetornarNullCuandoNoExiste()
    {
        var resultado = await _service.Buscar(999);

        Assert.True(resultado.IsSuccess);
        Assert.Null(resultado.Value);
    }

    [Fact]
    public async Task Guardar_DebeRegistrarVueloNuevo()
    {
        int aerolineaId;
        int destinoId;

        using (var context = _factory.CreateDbContext())
        {
            var aerolinea = new Aerolinea
            {
                Nombre = "American Airlines",
                Pais = "Estados Unidos"
            };

            var destino = new Destino
            {
                Ciudad = "Nueva York",
                Pais = "Estados Unidos"
            };

            context.Aerolineas.Add(aerolinea);
            context.Destinos.Add(destino);
            await context.SaveChangesAsync();

            aerolineaId = aerolinea.AerolineaId;
            destinoId = destino.DestinoId;
        }

        var vuelo = CrearVuelo("AA500", aerolineaId, destinoId);

        var resultado = await _service.Guardar(vuelo);

        Assert.True(resultado.IsSuccess);

        using var nuevoContext = _factory.CreateDbContext();

        var guardado = await nuevoContext.Vuelos
            .FirstOrDefaultAsync(x => x.NumeroVuelo == "AA500");

        Assert.NotNull(guardado);
        Assert.Equal(aerolineaId, guardado.AerolineaId);
        Assert.Equal(destinoId, guardado.DestinoId);
        Assert.Equal(120, guardado.Precio);
        Assert.Equal(150, guardado.AsientosDisponibles);
    }

    [Fact]
    public async Task Guardar_DebeActualizarVueloExistente()
    {
        int id;
        int aerolineaId;
        int destinoId;

        using (var context = _factory.CreateDbContext())
        {
            var aerolinea = new Aerolinea
            {
                Nombre = "Air Europa",
                Pais = "Espana"
            };

            var destino = new Destino
            {
                Ciudad = "Madrid",
                Pais = "Espana"
            };

            context.Aerolineas.Add(aerolinea);
            context.Destinos.Add(destino);
            await context.SaveChangesAsync();

            aerolineaId = aerolinea.AerolineaId;
            destinoId = destino.DestinoId;

            var vuelo = CrearVuelo("UX100", aerolineaId, destinoId);

            context.Vuelos.Add(vuelo);
            await context.SaveChangesAsync();

            id = vuelo.VueloId;
        }

        var actualizado = CrearVuelo("UX200", aerolineaId, destinoId);
        actualizado.VueloId = id;
        actualizado.Precio = 250;
        actualizado.AsientosDisponibles = 100;

        var resultado = await _service.Guardar(actualizado);

        Assert.True(resultado.IsSuccess);

        using var nuevoContext = _factory.CreateDbContext();

        var vueloGuardado = await nuevoContext.Vuelos.FindAsync(id);

        Assert.NotNull(vueloGuardado);
        Assert.Equal("UX200", vueloGuardado.NumeroVuelo);
        Assert.Equal(250, vueloGuardado.Precio);
        Assert.Equal(100, vueloGuardado.AsientosDisponibles);
        Assert.Equal(aerolineaId, vueloGuardado.AerolineaId);
        Assert.Equal(destinoId, vueloGuardado.DestinoId);
    }

    [Fact]
    public async Task Eliminar_DebeEliminarVueloExistente()
    {
        int id;
        int aerolineaId;
        int destinoId;

        using (var context = _factory.CreateDbContext())
        {
            var aerolinea = new Aerolinea
            {
                Nombre = "Copa Airlines",
                Pais = "Panama"
            };

            var destino = new Destino
            {
                Ciudad = "Panama",
                Pais = "Panama"
            };

            context.Aerolineas.Add(aerolinea);
            context.Destinos.Add(destino);
            await context.SaveChangesAsync();

            aerolineaId = aerolinea.AerolineaId;
            destinoId = destino.DestinoId;

            var vuelo = CrearVuelo("CM300", aerolineaId, destinoId);

            context.Vuelos.Add(vuelo);
            await context.SaveChangesAsync();

            id = vuelo.VueloId;
        }

        var resultado = await _service.Eliminar(id);

        Assert.True(resultado.IsSuccess);

        using var nuevoContext = _factory.CreateDbContext();

        var eliminado = await nuevoContext.Vuelos.FindAsync(id);

        Assert.Null(eliminado);
    }

    [Fact]
    public async Task Eliminar_DebeRetornarErrorCuandoVueloNoExiste()
    {
        var resultado = await _service.Eliminar(999);

        Assert.False(resultado.IsSuccess);
    }

    private static Vuelo CrearVuelo(
        string numeroVuelo,
        int aerolineaId,
        int destinoId)
    {
        return new Vuelo
        {
            NumeroVuelo = numeroVuelo,
            AerolineaId = aerolineaId,
            DestinoId = destinoId,
            FechaSalida = new DateTime(2026, 12, 10),
            FechaLlegada = new DateTime(2026, 12, 10, 15, 0, 0),
            AsientosDisponibles = 150,
            Precio = 120
        };
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
