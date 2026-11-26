using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ViajaYa.Data;
using ViajaYa.Models;
using ViajaYa.Services;

namespace ViajaYa.Tests;

public class ReservaHotelServiceTests : IDisposable
{
    private readonly SqliteConnection _conexion;
    private readonly TestDbContextFactory _factory;
    private readonly ReservaHotelService _service;

    public ReservaHotelServiceTests()
    {
        _conexion = new SqliteConnection("Data Source=:memory:");
        _conexion.Open();

        var opciones = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_conexion)
            .Options;

        using var context = new ApplicationDbContext(opciones);
        context.Database.EnsureCreated();

        _factory = new TestDbContextFactory(opciones);
        _service = new ReservaHotelService(_factory);
    }

    [Fact]
    public async Task GetList_DebeObtenerReservasConSusRelaciones()
    {
        var datos = await CrearDatosIniciales();

        var reserva = CrearReserva(
            datos.clienteId, datos.agenteId, datos.hotelId, 2);

        var guardar = await _service.Guardar(reserva);

        Assert.True(guardar.IsSuccess);

        var resultado = await _service.GetList(
            x => x.ClienteId == datos.clienteId);

        Assert.True(resultado.IsSuccess);
        Assert.Single(resultado.Value);

        var encontrada = resultado.Value[0];

        Assert.NotNull(encontrada.Cliente);
        Assert.NotNull(encontrada.Agente);
        Assert.Single(encontrada.Detalles);
        Assert.NotNull(encontrada.Detalles[0].Hotel);

        Assert.Equal("Carlos Perez", encontrada.Cliente!.Nombre);
        Assert.Equal("Juan", encontrada.Agente!.Nombre);
        Assert.Equal("Hotel Atlantico", encontrada.Detalles[0].Hotel!.Nombre);
    }

    [Fact]
    public async Task Buscar_DebeEncontrarReservaExistente()
    {
        var datos = await CrearDatosIniciales();

        var reserva = CrearReserva(
            datos.clienteId, datos.agenteId, datos.hotelId, 2);

        await _service.Guardar(reserva);

        var resultado = await _service.Buscar(reserva.ReservaHotelId);

        Assert.True(resultado.IsSuccess);
        Assert.NotNull(resultado.Value);
        Assert.Equal(datos.clienteId, resultado.Value.ClienteId);
        Assert.Equal(datos.agenteId, resultado.Value.AgenteId);
        Assert.Single(resultado.Value.Detalles);
        Assert.NotNull(resultado.Value.Detalles[0].Hotel);
    }

    [Fact]
    public async Task Buscar_DebeRetornarNullCuandoNoExiste()
    {
        var resultado = await _service.Buscar(999);

        Assert.True(resultado.IsSuccess);
        Assert.Null(resultado.Value);
    }

    [Fact]
    public async Task Guardar_DebeRegistrarReservaYDescontarHabitaciones()
    {
        var datos = await CrearDatosIniciales(20);

        var reserva = CrearReserva(
            datos.clienteId, datos.agenteId, datos.hotelId, 3);

        var resultado = await _service.Guardar(reserva);

        Assert.True(resultado.IsSuccess);

        using var context = _factory.CreateDbContext();

        var hotel = await context.Hoteles.FindAsync(datos.hotelId);

        var reservaGuardada = await context.ReservaHoteles
            .Include(x => x.Detalles)
            .FirstOrDefaultAsync(x => x.ReservaHotelId == reserva.ReservaHotelId);

        Assert.NotNull(hotel);
        Assert.Equal(17, hotel.HabitacionesDisponibles);

        Assert.NotNull(reservaGuardada);
        Assert.Single(reservaGuardada.Detalles);
        Assert.Equal(3, reservaGuardada.Detalles[0].CantidadHabitaciones);
    }

    [Fact]
    public async Task Guardar_DebeRechazarHotelInexistente()
    {
        var datos = await CrearDatosIniciales();

        var reserva = CrearReserva(
            datos.clienteId, datos.agenteId, 999, 2);

        var resultado = await _service.Guardar(reserva);

        Assert.False(resultado.IsSuccess);

        using var context = _factory.CreateDbContext();

        Assert.Empty(await context.ReservaHoteles.ToListAsync());
    }

    [Fact]
    public async Task Guardar_DebeRechazarReservaSinSuficientesHabitaciones()
    {
        var datos = await CrearDatosIniciales(2);

        var reserva = CrearReserva(
            datos.clienteId, datos.agenteId, datos.hotelId, 5);

        var resultado = await _service.Guardar(reserva);

        Assert.False(resultado.IsSuccess);

        using var context = _factory.CreateDbContext();

        var hotel = await context.Hoteles.FindAsync(datos.hotelId);

        Assert.NotNull(hotel);
        Assert.Equal(2, hotel.HabitacionesDisponibles);
        Assert.Empty(await context.ReservaHoteles.ToListAsync());
    }

    [Fact]
    public async Task Guardar_DebeActualizarReservaYRestablecerHabitacionesAnteriores()
    {
        var datos = await CrearDatosIniciales(20);

        var reserva = CrearReserva(
            datos.clienteId, datos.agenteId, datos.hotelId, 3);

        var guardar = await _service.Guardar(reserva);

        Assert.True(guardar.IsSuccess);

        var actualizada = CrearReserva(
            datos.clienteId, datos.agenteId, datos.hotelId, 5);

        actualizada.ReservaHotelId = reserva.ReservaHotelId;

        var resultado = await _service.Guardar(actualizada);

        Assert.True(resultado.IsSuccess);

        using var context = _factory.CreateDbContext();

        var hotel = await context.Hoteles.FindAsync(datos.hotelId);

        var reservaGuardada = await context.ReservaHoteles
            .Include(x => x.Detalles)
            .FirstOrDefaultAsync(x => x.ReservaHotelId == reserva.ReservaHotelId);

        Assert.NotNull(hotel);
        Assert.Equal(15, hotel.HabitacionesDisponibles);

        Assert.NotNull(reservaGuardada);
        Assert.Single(reservaGuardada.Detalles);
        Assert.Equal(5, reservaGuardada.Detalles[0].CantidadHabitaciones);
    }

    [Fact]
    public async Task Guardar_DebeRetornarErrorCuandoReservaNoExiste()
    {
        var datos = await CrearDatosIniciales();

        var reserva = CrearReserva(
            datos.clienteId, datos.agenteId, datos.hotelId, 2);

        reserva.ReservaHotelId = 999;

        var resultado = await _service.Guardar(reserva);

        Assert.False(resultado.IsSuccess);
    }

    [Fact]
    public async Task Guardar_DebeRechazarActualizacionSinDisponibilidad()
    {
        var datos = await CrearDatosIniciales(5);

        var reserva = CrearReserva(
            datos.clienteId, datos.agenteId, datos.hotelId, 2);

        var guardar = await _service.Guardar(reserva);

        Assert.True(guardar.IsSuccess);

        var actualizada = CrearReserva(
            datos.clienteId, datos.agenteId, datos.hotelId, 6);

        actualizada.ReservaHotelId = reserva.ReservaHotelId;

        var resultado = await _service.Guardar(actualizada);

        Assert.False(resultado.IsSuccess);

        using var context = _factory.CreateDbContext();

        var hotel = await context.Hoteles.FindAsync(datos.hotelId);

        var reservaGuardada = await context.ReservaHoteles
            .Include(x => x.Detalles)
            .FirstOrDefaultAsync(x => x.ReservaHotelId == reserva.ReservaHotelId);

        Assert.NotNull(hotel);
        Assert.Equal(3, hotel.HabitacionesDisponibles);

        Assert.NotNull(reservaGuardada);
        Assert.Single(reservaGuardada.Detalles);
        Assert.Equal(2, reservaGuardada.Detalles[0].CantidadHabitaciones);
    }

    [Fact]
    public async Task Eliminar_DebeEliminarReservaYDevolverHabitaciones()
    {
        var datos = await CrearDatosIniciales(20);

        var reserva = CrearReserva(
            datos.clienteId, datos.agenteId, datos.hotelId, 4);

        var guardar = await _service.Guardar(reserva);

        Assert.True(guardar.IsSuccess);

        var resultado = await _service.Eliminar(reserva.ReservaHotelId);

        Assert.True(resultado.IsSuccess);

        using var context = _factory.CreateDbContext();

        var hotel = await context.Hoteles.FindAsync(datos.hotelId);

        var reservaEliminada = await context.ReservaHoteles.FindAsync(
            reserva.ReservaHotelId);

        Assert.NotNull(hotel);
        Assert.Equal(20, hotel.HabitacionesDisponibles);
        Assert.Null(reservaEliminada);
    }

    [Fact]
    public async Task Eliminar_DebeRetornarErrorCuandoReservaNoExiste()
    {
        var resultado = await _service.Eliminar(999);

        Assert.False(resultado.IsSuccess);
    }

    private async Task<(int clienteId, int agenteId, int hotelId)>
        CrearDatosIniciales(int habitacionesDisponibles = 20)
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

        var destino = new Destino
        {
            Ciudad = "Punta Cana",
            Pais = "Republica Dominicana"
        };

        context.Clientes.Add(cliente);
        context.Agentes.Add(agente);
        context.Destinos.Add(destino);

        await context.SaveChangesAsync();

        var hotel = new Hotel
        {
            Nombre = "Hotel Atlantico",
            Direccion = "Avenida Principal",
            Categoria = 5,
            DestinoId = destino.DestinoId,
            HabitacionesDisponibles = habitacionesDisponibles,
            PrecioPorNoche = 150
        };

        context.Hoteles.Add(hotel);
        await context.SaveChangesAsync();

        return (
            cliente.ClienteId,
            agente.AgenteId,
            hotel.HotelId
        );
    }

    private static ReservaHotel CrearReserva(
        int clienteId,
        int agenteId,
        int hotelId,
        int cantidadHabitaciones)
    {
        return new ReservaHotel
        {
            ClienteId = clienteId,
            AgenteId = agenteId,
            FechaReserva = new DateTime(2026, 11, 24),
            Estado = "Confirmada",
            Detalles = new List<DetalleReservaHotel>
            {
                new()
                {
                    HotelId = hotelId,
                    CantidadHabitaciones = cantidadHabitaciones,
                    CantidadNoches = 2,
                    PrecioUnitario = 150
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
