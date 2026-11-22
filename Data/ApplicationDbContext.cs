using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ViajaYa.Models;

namespace ViajaYa.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Agente> Agentes { get; set; }
    public DbSet<Aerolinea> Aerolineas { get; set; }
    public DbSet<Destino> Destinos { get; set; }
    public DbSet<Vuelo> Vuelos { get; set; }
    public DbSet<Hotel> Hoteles { get; set; }
    public DbSet<ReservaVuelo> ReservaVuelos { get; set; }
    public DbSet<DetalleReservaVuelo> DetalleReservaVuelos { get; set; }
    public DbSet<ReservaHotel> ReservaHoteles { get; set; }
    public DbSet<DetalleReservaHotel> DetalleReservaHoteles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}