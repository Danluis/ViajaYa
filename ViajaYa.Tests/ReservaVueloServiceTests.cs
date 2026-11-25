using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ViajaYa.Data;
using ViajaYa.Models;
using ViajaYa.Services;

namespace ViajaYa.Tests;

public class ReservaVueloServiceTests : IDisposable
{
    private readonly SqliteConnection _conexion;
    private readonly TestDbContextFactory _factory;
    private readonly ReservaVueloService _service;

    public ReservaVueloServiceTests()
    {
        _conexion = new SqliteConnection("Data Source=:memory:");
        _conexion.Open();

        var opciones = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_conexion)
            .Options;

        using var context = new ApplicationDbContext(opciones);
        context.Database.EnsureCreated();

        _factory = new TestDbContextFactory(opciones);
        _service = new ReservaVueloService(_factory);
    }

    [Fact]
    public async Task GetList_DebeObtenerReservasConSusRelaciones()
    {
        var datos = await CrearDatosIniciales();

        var reserva = CrearReserva(datos.clienteId, datos.agenteId, datos.vueloId, 2);

        var guardar = await _service.Guardar(reserva);

        Assert.True(guardar.IsSuccess);

        var resultado = await _service.GetList(x => x.ClienteId == datos.clienteId);

        Assert.True(resultado.IsSuccess);
        Assert.Single(resultado.Value);

        var encontrada = resultado.Value[0];

        Assert.NotNull(encontrada.Cliente);
        Assert.NotNull(encontrada.Agente);
        Assert.Single(encontrada.Detalles);
        Assert.NotNull(encontrada.Detalles[0].Vuelo);

        Assert.Equal("Carlos Perez", encontrada.Cliente!.Nombre);
        Assert.Equal("Juan", encontrada.Agente!.Nombre);
        Assert.Equal("AV100", encontrada.Detalles[0].Vuelo!.NumeroVuelo);
    }

    [Fact]
    public async Task Buscar_DebeEncontrarReservaExistente()
    {
        var datos = await CrearDatosIniciales();

        var reserva = CrearReserva(datos.clienteId, datos.agenteId, datos.vueloId, 2);

        await _service.Guardar(reserva);

        var resultado = await _service.Buscar(reserva.ReservaVueloId);

        Assert.True(resultado.IsSuccess);
        Assert.NotNull(resultado.Value);
        Assert.Equal(datos.clienteId, resultado.Value.ClienteId);
        Assert.Equal(datos.agenteId, resultado.Value.AgenteId);
        Assert.Single(resultado.Value.Detalles);
    }

    [Fact]
    public async Task Buscar_DebeRetornarNullCuandoNoExiste()
    {
        var resultado = await _service.Buscar(999);

        Assert.True(resultado.IsSuccess);
        Assert.Null(resultado.Value);
    }

    [Fact]
    public async Task Guardar_DebeRegistrarReservaYDescontarAsientos()
    {
        var datos = await CrearDatosIniciales(20);

        var reserva = CrearReserva(datos.clienteId, datos.agenteId, datos.vueloId, 3);

        var resultado = await _service.Guardar(reserva);

        Assert.True(resultado.IsSuccess);

        using var context = _factory.CreateDbContext();

        var vuelo = await context.Vuelos.FindAsync(datos.vueloId);
        var reservaGuardada = await context.ReservaVuelos
            .Include(x => x.Detalles)
            .FirstOrDefaultAsync(x => x.ReservaVueloId == reserva.ReservaVueloId);

        Assert.NotNull(vuelo);
        Assert.Equal(17, vuelo.AsientosDisponibles);

        Assert.NotNull(reservaGuardada);
        Assert.Single(reservaGuardada.Detalles);
        Assert.Equal(3, reservaGuardada.Detalles[0].CantidadAsientos);
    }

    [Fact]
    public async Task Guardar_DebeRechazarCantidadDeAsientosInvalida()
    {
        var datos = await CrearDatosIniciales(20);

        var reserva = CrearReserva(datos.clienteId, datos.agenteId, datos.vueloId, 0);

        var resultado = await _service.Guardar(reserva);

        Assert.False(resultado.IsSuccess);

        using var context = _factory.CreateDbContext();

        var vuelo = await context.Vuelos.FindAsync(datos.vueloId);

        Assert.NotNull(vuelo);
        Assert.Equal(20, vuelo.AsientosDisponibles);
        Assert.Empty(await context.ReservaVuelos.ToListAsync());
    }

    [Fact]
    public async Task Guardar_DebeRechazarVueloInexistente()
    {
        var datos = await CrearDatosIniciales();

        var reserva = CrearReserva(datos.clienteId, datos.agenteId, 999, 2);

        var resultado = await _service.Guardar(reserva);

        Assert.False(resultado.IsSuccess);

        using var context = _factory.CreateDbContext();

        Assert.Empty(await context.ReservaVuelos.ToListAsync());
    }

    [Fact]
    public async Task Guardar_DebeRechazarReservaSinSuficientesAsientos()
    {
        var datos = await CrearDatosIniciales(2);

        var reserva = CrearReserva(datos.clienteId, datos.agenteId, datos.vueloId, 5);

        var resultado = await _service.Guardar(reserva);

        Assert.False(resultado.IsSuccess);

        using var context = _factory.CreateDbContext();

        var vuelo = await context.Vuelos.FindAsync(datos.vueloId);

        Assert.NotNull(vuelo);
        Assert.Equal(2, vuelo.AsientosDisponibles);
        Assert.Empty(await context.ReservaVuelos.ToListAsync());
    }

    [Fact]
    public async Task Guardar_DebeActualizarReservaYDevolverAsientosAnteriores()
    {
        var datos = await CrearDatosIniciales(20);

        var reserva = CrearReserva(datos.clienteId, datos.agenteId, datos.vueloId, 3);

        var guardar = await _service.Guardar(reserva);

        Assert.True(guardar.IsSuccess);

        var actualizada = CrearReserva(
            datos.clienteId,
            datos.agenteId,
            datos.vueloId,
            5);

        actualizada.ReservaVueloId = reserva.ReservaVueloId;
        actualizada.Estado = "Confirmada";

        var resultado = await _service.Guardar(actualizada);

        Assert.True(resultado.IsSuccess);

        using var context = _factory.CreateDbContext();

        var vuelo = await context.Vuelos.FindAsync(datos.vueloId);

        var reservaGuardada = await context.ReservaVuelos
            .Include(x => x.Detalles)
            .FirstOrDefaultAsync(x => x.ReservaVueloId == reserva.ReservaVueloId);

        Assert.NotNull(vuelo);
        Assert.Equal(15, vuelo.AsientosDisponibles);

        Assert.NotNull(reservaGuardada);
        Assert.Single(reservaGuardada.Detalles);
        Assert.Equal(5, reservaGuardada.Detalles[0].CantidadAsientos);
    }

    [Fact]
    public async Task Guardar_DebeRegistrarReservaCanceladaSinDescontarAsientos()
    {
        var datos = await CrearDatosIniciales(20);

        var reserva = CrearReserva(datos.clienteId, datos.agenteId, datos.vueloId, 3);
        reserva.Estado = "Cancelada";

        var resultado = await _service.Guardar(reserva);

        Assert.True(resultado.IsSuccess);

        using var context = _factory.CreateDbContext();

        var vuelo = await context.Vuelos.FindAsync(datos.vueloId);
        var reservaGuardada = await context.ReservaVuelos.FindAsync(
            reserva.ReservaVueloId);

        Assert.NotNull(vuelo);
        Assert.Equal(20, vuelo.AsientosDisponibles);

        Assert.NotNull(reservaGuardada);
        Assert.Equal("Cancelada", reservaGuardada.Estado);
    }

    [Fact]
    public async Task Eliminar_DebeEliminarReservaYDevolverAsientos()
    {
        var datos = await CrearDatosIniciales(20);

        var reserva = CrearReserva(datos.clienteId, datos.agenteId, datos.vueloId, 4);

        var guardar = await _service.Guardar(reserva);

        Assert.True(guardar.IsSuccess);

        var resultado = await _service.Eliminar(reserva.ReservaVueloId);

        Assert.True(resultado.IsSuccess);

        using var context = _factory.CreateDbContext();

        var vuelo = await context.Vuelos.FindAsync(datos.vueloId);
        var reservaEliminada = await context.ReservaVuelos.FindAsync(
            reserva.ReservaVueloId);

        Assert.NotNull(vuelo);
        Assert.Equal(20, vuelo.AsientosDisponibles);
        Assert.Null(reservaEliminada);
    }

    [Fact]
    public async Task Eliminar_DebeEliminarReservaCanceladaSinDevolverAsientos()
    {
        var datos = await CrearDatosIniciales(20);

        var reserva = CrearReserva(datos.clienteId, datos.agenteId, datos.vueloId, 3);
        reserva.Estado = "Cancelada";

        var guardar = await _service.Guardar(reserva);

        Assert.True(guardar.IsSuccess);

        var resultado = await _service.Eliminar(reserva.ReservaVueloId);

        Assert.True(resultado.IsSuccess);

        using var context = _factory.CreateDbContext();

        var vuelo = await context.Vuelos.FindAsync(datos.vueloId);
        var reservaEliminada = await context.ReservaVuelos.FindAsync(
            reserva.ReservaVueloId);

        Assert.NotNull(vuelo);
        Assert.Equal(20, vuelo.AsientosDisponibles);
        Assert.Null(reservaEliminada);
    }

    [Fact]
    public async Task Eliminar_DebeRetornarErrorCuandoReservaNoExiste()
    {
        var resultado = await _service.Eliminar(999);

        Assert.False(resultado.IsSuccess);
    }

    private async Task<(int clienteId, int agenteId, int vueloId)>
        CrearDatosIniciales(int asientosDisponibles = 20)
    {
        using var context = _factory.CreateDbContext();

        var cliente = new Cliente
        {
            Cedula = "00100000001",
            Nombre = "Carlos Perez",
            Telefono = "8095551234",
            Email = "carlos@gmail.com"
        };

        var agente = new Agente
        {
            Nombre = "Juan",
            Codigo = "AG001"
        };

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

        context.Clientes.Add(cliente);
        context.Agentes.Add(agente);
        context.Aerolineas.Add(aerolinea);
        context.Destinos.Add(destino);

        await context.SaveChangesAsync();

        var vuelo = new Vuelo
        {
            NumeroVuelo = "AV100",
            AerolineaId = aerolinea.AerolineaId,
            DestinoId = destino.DestinoId,
            FechaSalida = new DateTime(2026, 12, 10),
            FechaLlegada = new DateTime(2026, 12, 10, 15, 0, 0),
            AsientosDisponibles = asientosDisponibles,
            Precio = 120
        };

        context.Vuelos.Add(vuelo);
        await context.SaveChangesAsync();

        return (
            cliente.ClienteId,
            agente.AgenteId,
            vuelo.VueloId
        );
    }

    private static ReservaVuelo CrearReserva(
        int clienteId,
        int agenteId,
        int vueloId,
        int cantidadAsientos)
    {
        return new ReservaVuelo
        {
            ClienteId = clienteId,
            AgenteId = agenteId,
            FechaReserva = new DateTime(2026, 11, 24),
            Estado = "Confirmada",
            Detalles = new List<DetalleReservaVuelo>
            {
                new()
                {
                    VueloId = vueloId,
                    CantidadAsientos = cantidadAsientos,
                    PrecioUnitario = 120
                }
            }
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
