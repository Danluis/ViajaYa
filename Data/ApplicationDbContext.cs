
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ViajaYa.Models;

namespace ViajaYa.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Cliente> Clientes { get; set; }

    public DbSet<Aeronave> Aeronaves { get; set; }

    public DbSet<Piloto> Pilotos { get; set; }

    public DbSet<Vuelo> Vuelos { get; set; }

    public DbSet<Reserva> Reservas { get; set; }

    public DbSet<Pago> Pagos { get; set; }

    public DbSet<Tarifa> Tarifas { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Matricula unica para cada aeronave
        builder.Entity<Aeronave>()
            .HasIndex(a => a.Matricula)
            .IsUnique();

        // Codigo unico para cada reserva
        builder.Entity<Reserva>()
            .HasIndex(r => r.Codigo)
            .IsUnique();

        // Relacion entre cliente y reservas
        builder.Entity<Reserva>()
            .HasOne(r => r.Cliente)
            .WithMany(c => c.Reservas)
            .HasForeignKey(r => r.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacion entre cliente y pagos
        builder.Entity<Pago>()
            .HasOne(p => p.Cliente)
            .WithMany(c => c.Pagos)
            .HasForeignKey(p => p.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacion entre piloto y vuelos
        builder.Entity<Vuelo>()
            .HasOne(v => v.Piloto)
            .WithMany(p => p.Vuelos)
            .HasForeignKey(v => v.PilotoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacion entre aeronave y vuelos
        builder.Entity<Vuelo>()
            .HasOne(v => v.Aeronave)
            .WithMany(a => a.Vuelos)
            .HasForeignKey(v => v.AeronaveId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacion entre aeronave y reservas
        builder.Entity<Reserva>()
            .HasOne(r => r.Aeronave)
            .WithMany(a => a.Reservas)
            .HasForeignKey(r => r.AeronaveId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacion entre vuelo y reservas
        builder.Entity<Reserva>()
            .HasOne(r => r.Vuelo)
            .WithMany(v => v.Reservas)
            .HasForeignKey(r => r.VueloId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacion entre reserva y pagos
        builder.Entity<Pago>()
            .HasOne(p => p.Reserva)
            .WithMany(r => r.Pagos)
            .HasForeignKey(p => p.ReservaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
