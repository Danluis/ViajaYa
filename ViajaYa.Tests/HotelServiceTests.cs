using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ViajaYa.Data;
using ViajaYa.Models;
using ViajaYa.Services;

namespace ViajaYa.Tests;

public class HotelServiceTests : IDisposable
{
    private readonly SqliteConnection _conexion;
    private readonly TestDbContextFactory _factory;
    private readonly HotelService _service;

    public HotelServiceTests()
    {
        _conexion = new SqliteConnection("Data Source=:memory:");
        _conexion.Open();

        var opciones = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_conexion)
            .Options;

        using var context = new ApplicationDbContext(opciones);
        context.Database.EnsureCreated();

        _factory = new TestDbContextFactory(opciones);
        _service = new HotelService(_factory);
    }

    [Fact]
    public async Task GetList_DebeObtenerHotelesSegunFiltro()
    {
        int destinoId;

        using (var context = _factory.CreateDbContext())
        {
            var destino = new Destino
            {
                Ciudad = "Punta Cana",
                Pais = "Republica Dominicana"
            };

            context.Destinos.Add(destino);
            await context.SaveChangesAsync();

            destinoId = destino.DestinoId;

            context.Hoteles.AddRange(
                CrearHotel("Hotel Caribe", destinoId),
                CrearHotel("Hotel Colonial", destinoId)
            );

            await context.SaveChangesAsync();
        }

        var resultado = await _service.GetList(x => x.Nombre == "Hotel Caribe");

        Assert.True(resultado.IsSuccess);
        Assert.Single(resultado.Value);
        Assert.Equal("Hotel Caribe", resultado.Value[0].Nombre);
        Assert.NotNull(resultado.Value[0].Destino);
        Assert.Equal("Punta Cana", resultado.Value[0].Destino!.Ciudad);
    }

    [Fact]
    public async Task Buscar_DebeEncontrarHotelExistente()
    {
        int id;
        int destinoId;

        using (var context = _factory.CreateDbContext())
        {
            var destino = new Destino
            {
                Ciudad = "Santo Domingo",
                Pais = "Republica Dominicana"
            };

            context.Destinos.Add(destino);
            await context.SaveChangesAsync();

            destinoId = destino.DestinoId;

            var hotel = CrearHotel("Hotel Nacional", destinoId);

            context.Hoteles.Add(hotel);
            await context.SaveChangesAsync();

            id = hotel.HotelId;
        }

        var resultado = await _service.Buscar(id);

        Assert.True(resultado.IsSuccess);
        Assert.NotNull(resultado.Value);
        Assert.Equal("Hotel Nacional", resultado.Value.Nombre);
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
    public async Task Guardar_DebeRegistrarHotelNuevo()
    {
        int destinoId;

        using (var context = _factory.CreateDbContext())
        {
            var destino = new Destino
            {
                Ciudad = "Puerto Plata",
                Pais = "Republica Dominicana"
            };

            context.Destinos.Add(destino);
            await context.SaveChangesAsync();

            destinoId = destino.DestinoId;
        }

        var hotel = CrearHotel("Hotel Atlantico", destinoId);

        var resultado = await _service.Guardar(hotel);

        Assert.True(resultado.IsSuccess);

        using var nuevoContext = _factory.CreateDbContext();

        var guardado = await nuevoContext.Hoteles
            .FirstOrDefaultAsync(x => x.Nombre == "Hotel Atlantico");

        Assert.NotNull(guardado);
        Assert.Equal(destinoId, guardado.DestinoId);
        Assert.Equal(5, guardado.Categoria);
        Assert.Equal(150, guardado.PrecioPorNoche);
    }

    [Fact]
    public async Task Guardar_DebeActualizarHotelExistente()
    {
        int id;
        int destinoId;

        using (var context = _factory.CreateDbContext())
        {
            var destino = new Destino
            {
                Ciudad = "Punta Cana",
                Pais = "Republica Dominicana"
            };

            context.Destinos.Add(destino);
            await context.SaveChangesAsync();

            destinoId = destino.DestinoId;

            var hotel = CrearHotel("Hotel Caribe", destinoId);

            context.Hoteles.Add(hotel);
            await context.SaveChangesAsync();

            id = hotel.HotelId;
        }

        var actualizado = CrearHotel("Hotel Caribe Renovado", destinoId);
        actualizado.HotelId = id;
        actualizado.Categoria = 4;
        actualizado.PrecioPorNoche = 250;

        var resultado = await _service.Guardar(actualizado);

        Assert.True(resultado.IsSuccess);

        using var nuevoContext = _factory.CreateDbContext();

        var hotelGuardado = await nuevoContext.Hoteles.FindAsync(id);

        Assert.NotNull(hotelGuardado);
        Assert.Equal("Hotel Caribe Renovado", hotelGuardado.Nombre);
        Assert.Equal(4, hotelGuardado.Categoria);
        Assert.Equal(250, hotelGuardado.PrecioPorNoche);
    }

    [Fact]
    public async Task Eliminar_DebeEliminarHotelExistente()
    {
        int id;
        int destinoId;

        using (var context = _factory.CreateDbContext())
        {
            var destino = new Destino
            {
                Ciudad = "La Romana",
                Pais = "Republica Dominicana"
            };

            context.Destinos.Add(destino);
            await context.SaveChangesAsync();

            destinoId = destino.DestinoId;

            var hotel = CrearHotel("Hotel Romana", destinoId);

            context.Hoteles.Add(hotel);
            await context.SaveChangesAsync();

            id = hotel.HotelId;
        }

        var resultado = await _service.Eliminar(id);

        Assert.True(resultado.IsSuccess);

        using var nuevoContext = _factory.CreateDbContext();

        var eliminado = await nuevoContext.Hoteles.FindAsync(id);

        Assert.Null(eliminado);
    }

    [Fact]
    public async Task Eliminar_DebeRetornarErrorCuandoHotelNoExiste()
    {
        var resultado = await _service.Eliminar(999);

        Assert.False(resultado.IsSuccess);
    }

    private static Hotel CrearHotel(string nombre, int destinoId)
    {
        return new Hotel
        {
            Nombre = nombre,
            Direccion = "Avenida Principal",
            Categoria = 5,
            DestinoId = destinoId,
            HabitacionesDisponibles = 50,
            PrecioPorNoche = 150
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
