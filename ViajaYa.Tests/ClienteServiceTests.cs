using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ViajaYa.Data;
using ViajaYa.Models;
using ViajaYa.Services;

namespace ViajaYa.Tests;

public class ClienteServiceTests : IDisposable
{
    private readonly SqliteConnection _conexion;
    private readonly TestDbContextFactory _factory;
    private readonly ClienteService _service;

    public ClienteServiceTests()
    {
        _conexion = new SqliteConnection("Data Source=:memory:");
        _conexion.Open();

        var opciones = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_conexion)
            .Options;

        using var context = new ApplicationDbContext(opciones);
        context.Database.EnsureCreated();

        _factory = new TestDbContextFactory(opciones);
        _service = new ClienteService(_factory);
    }

    [Fact]
    public async Task GetList_DebeObtenerClientesSegunFiltro()
    {
        using (var context = _factory.CreateDbContext())
        {
            context.Clientes.AddRange(
                new Cliente
                {
                    Cedula = "00112345678",
                    Nombre = "Carlos Perez",
                    Telefono = "8091234567",
                    Email = "carlos@gmail.com"
                },
                new Cliente
                {
                    Cedula = "00212345678",
                    Nombre = "Maria Lopez",
                    Telefono = "8291234567",
                    Email = "maria@gmail.com"
                }
            );

            await context.SaveChangesAsync();
        }

        var resultado = await _service.GetList(x => x.Nombre == "Carlos Perez");

        Assert.True(resultado.IsSuccess);
        Assert.Single(resultado.Value);
        Assert.Equal("Carlos Perez", resultado.Value[0].Nombre);
    }

    [Fact]
    public async Task Buscar_DebeEncontrarClienteExistente()
    {
        int id;

        using (var context = _factory.CreateDbContext())
        {
            var cliente = new Cliente
            {
                Cedula = "00112345678",
                Nombre = "Carlos Perez",
                Telefono = "8091234567",
                Email = "carlos@gmail.com"
            };

            context.Clientes.Add(cliente);
            await context.SaveChangesAsync();

            id = cliente.ClienteId;
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
    public async Task Guardar_DebeRegistrarClienteNuevo()
    {
        var cliente = new Cliente
        {
            Cedula = "00312345678",
            Nombre = "Pedro Gomez",
            Telefono = "8491234567",
            Email = "pedro@gmail.com"
        };

        var resultado = await _service.Guardar(cliente);

        Assert.True(resultado.IsSuccess);

        using var context = _factory.CreateDbContext();

        var guardado = await context.Clientes
            .FirstOrDefaultAsync(x => x.Cedula == "00312345678");

        Assert.NotNull(guardado);
        Assert.Equal("Pedro Gomez", guardado.Nombre);
        Assert.Equal("pedro@gmail.com", guardado.Email);
    }

    [Fact]
    public async Task Guardar_DebeActualizarClienteExistente()
    {
        int id;

        using (var context = _factory.CreateDbContext())
        {
            var cliente = new Cliente
            {
                Cedula = "00312345678",
                Nombre = "Pedro Gomez",
                Telefono = "8491234567",
                Email = "pedro@gmail.com"
            };

            context.Clientes.Add(cliente);
            await context.SaveChangesAsync();

            id = cliente.ClienteId;
        }

        var actualizado = new Cliente
        {
            ClienteId = id,
            Cedula = "00312345678",
            Nombre = "Pedro Actualizado",
            Telefono = "8497654321",
            Email = "pedroactualizado@gmail.com"
        };

        var resultado = await _service.Guardar(actualizado);

        Assert.True(resultado.IsSuccess);

        using var nuevoContext = _factory.CreateDbContext();

        var clienteGuardado = await nuevoContext.Clientes.FindAsync(id);

        Assert.NotNull(clienteGuardado);
        Assert.Equal("Pedro Actualizado", clienteGuardado.Nombre);
        Assert.Equal("8497654321", clienteGuardado.Telefono);
        Assert.Equal("pedroactualizado@gmail.com", clienteGuardado.Email);
    }

    [Fact]
    public async Task Eliminar_DebeEliminarClienteExistente()
    {
        int id;

        using (var context = _factory.CreateDbContext())
        {
            var cliente = new Cliente
            {
                Cedula = "00312345678",
                Nombre = "Pedro Gomez",
                Telefono = "8491234567",
                Email = "pedro@gmail.com"
            };

            context.Clientes.Add(cliente);
            await context.SaveChangesAsync();

            id = cliente.ClienteId;
        }

        var resultado = await _service.Eliminar(id);

        Assert.True(resultado.IsSuccess);

        using var nuevoContext = _factory.CreateDbContext();

        var eliminado = await nuevoContext.Clientes.FindAsync(id);

        Assert.Null(eliminado);
    }

    [Fact]
    public async Task Eliminar_DebeRetornarErrorCuandoClienteNoExiste()
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
