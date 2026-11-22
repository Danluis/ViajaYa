using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViajaYa.Migrations;

/// <inheritdoc />
public partial class _20261122132508_ModeloNegocioAgencia : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Aerolineas",
            columns: table => new
            {
                AerolineaId = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                Pais = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Aerolineas", x => x.AerolineaId);
            });

        migrationBuilder.CreateTable(
            name: "Agentes",
            columns: table => new
            {
                AgenteId = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                Codigo = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Agentes", x => x.AgenteId);
            });

        migrationBuilder.CreateTable(
            name: "Clientes",
            columns: table => new
            {
                ClienteId = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                Cedula = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                Telefono = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Clientes", x => x.ClienteId);
            });

        migrationBuilder.CreateTable(
            name: "Destinos",
            columns: table => new
            {
                DestinoId = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Ciudad = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                Pais = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                Descripcion = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Destinos", x => x.DestinoId);
            });

        migrationBuilder.CreateTable(
            name: "ReservaHoteles",
            columns: table => new
            {
                ReservaHotelId = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                ClienteId = table.Column<int>(type: "INTEGER", nullable: false),
                AgenteId = table.Column<int>(type: "INTEGER", nullable: false),
                FechaEntrada = table.Column<DateTime>(type: "TEXT", nullable: false),
                FechaSalida = table.Column<DateTime>(type: "TEXT", nullable: false),
                Observacion = table.Column<string>(type: "TEXT", nullable: false),
                Total = table.Column<decimal>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ReservaHoteles", x => x.ReservaHotelId);
                table.ForeignKey(
                    name: "FK_ReservaHoteles_Agentes_AgenteId",
                    column: x => x.AgenteId,
                    principalTable: "Agentes",
                    principalColumn: "AgenteId",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_ReservaHoteles_Clientes_ClienteId",
                    column: x => x.ClienteId,
                    principalTable: "Clientes",
                    principalColumn: "ClienteId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ReservaVuelos",
            columns: table => new
            {
                ReservaVueloId = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                ClienteId = table.Column<int>(type: "INTEGER", nullable: false),
                AgenteId = table.Column<int>(type: "INTEGER", nullable: false),
                Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                Observacion = table.Column<string>(type: "TEXT", nullable: false),
                Total = table.Column<decimal>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ReservaVuelos", x => x.ReservaVueloId);
                table.ForeignKey(
                    name: "FK_ReservaVuelos_Agentes_AgenteId",
                    column: x => x.AgenteId,
                    principalTable: "Agentes",
                    principalColumn: "AgenteId",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_ReservaVuelos_Clientes_ClienteId",
                    column: x => x.ClienteId,
                    principalTable: "Clientes",
                    principalColumn: "ClienteId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Hoteles",
            columns: table => new
            {
                HotelId = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                DestinoId = table.Column<int>(type: "INTEGER", nullable: false),
                Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                Direccion = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                HabitacionesDisponibles = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Hoteles", x => x.HotelId);
                table.ForeignKey(
                    name: "FK_Hoteles_Destinos_DestinoId",
                    column: x => x.DestinoId,
                    principalTable: "Destinos",
                    principalColumn: "DestinoId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Vuelos",
            columns: table => new
            {
                VueloId = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                AerolineaId = table.Column<int>(type: "INTEGER", nullable: false),
                DestinoId = table.Column<int>(type: "INTEGER", nullable: false),
                NumeroVuelo = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                Precio = table.Column<decimal>(type: "TEXT", nullable: false),
                AsientosDisponibles = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Vuelos", x => x.VueloId);
                table.ForeignKey(
                    name: "FK_Vuelos_Aerolineas_AerolineaId",
                    column: x => x.AerolineaId,
                    principalTable: "Aerolineas",
                    principalColumn: "AerolineaId",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Vuelos_Destinos_DestinoId",
                    column: x => x.DestinoId,
                    principalTable: "Destinos",
                    principalColumn: "DestinoId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "DetalleReservaHoteles",
            columns: table => new
            {
                DetalleId = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                ReservaHotelId = table.Column<int>(type: "INTEGER", nullable: false),
                HotelId = table.Column<int>(type: "INTEGER", nullable: false),
                Cantidad = table.Column<int>(type: "INTEGER", nullable: false),
                Precio = table.Column<decimal>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DetalleReservaHoteles", x => x.DetalleId);
                table.ForeignKey(
                    name: "FK_DetalleReservaHoteles_Hoteles_HotelId",
                    column: x => x.HotelId,
                    principalTable: "Hoteles",
                    principalColumn: "HotelId",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_DetalleReservaHoteles_ReservaHoteles_ReservaHotelId",
                    column: x => x.ReservaHotelId,
                    principalTable: "ReservaHoteles",
                    principalColumn: "ReservaHotelId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "DetalleReservaVuelos",
            columns: table => new
            {
                DetalleId = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                ReservaVueloId = table.Column<int>(type: "INTEGER", nullable: false),
                VueloId = table.Column<int>(type: "INTEGER", nullable: false),
                Cantidad = table.Column<int>(type: "INTEGER", nullable: false),
                Precio = table.Column<decimal>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DetalleReservaVuelos", x => x.DetalleId);
                table.ForeignKey(
                    name: "FK_DetalleReservaVuelos_ReservaVuelos_ReservaVueloId",
                    column: x => x.ReservaVueloId,
                    principalTable: "ReservaVuelos",
                    principalColumn: "ReservaVueloId",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_DetalleReservaVuelos_Vuelos_VueloId",
                    column: x => x.VueloId,
                    principalTable: "Vuelos",
                    principalColumn: "VueloId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_DetalleReservaHoteles_HotelId",
            table: "DetalleReservaHoteles",
            column: "HotelId");

        migrationBuilder.CreateIndex(
            name: "IX_DetalleReservaHoteles_ReservaHotelId",
            table: "DetalleReservaHoteles",
            column: "ReservaHotelId");

        migrationBuilder.CreateIndex(
            name: "IX_DetalleReservaVuelos_ReservaVueloId",
            table: "DetalleReservaVuelos",
            column: "ReservaVueloId");

        migrationBuilder.CreateIndex(
            name: "IX_DetalleReservaVuelos_VueloId",
            table: "DetalleReservaVuelos",
            column: "VueloId");

        migrationBuilder.CreateIndex(
            name: "IX_Hoteles_DestinoId",
            table: "Hoteles",
            column: "DestinoId");

        migrationBuilder.CreateIndex(
            name: "IX_ReservaHoteles_AgenteId",
            table: "ReservaHoteles",
            column: "AgenteId");

        migrationBuilder.CreateIndex(
            name: "IX_ReservaHoteles_ClienteId",
            table: "ReservaHoteles",
            column: "ClienteId");

        migrationBuilder.CreateIndex(
            name: "IX_ReservaVuelos_AgenteId",
            table: "ReservaVuelos",
            column: "AgenteId");

        migrationBuilder.CreateIndex(
            name: "IX_ReservaVuelos_ClienteId",
            table: "ReservaVuelos",
            column: "ClienteId");

        migrationBuilder.CreateIndex(
            name: "IX_Vuelos_AerolineaId",
            table: "Vuelos",
            column: "AerolineaId");

        migrationBuilder.CreateIndex(
            name: "IX_Vuelos_DestinoId",
            table: "Vuelos",
            column: "DestinoId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "DetalleReservaHoteles");

        migrationBuilder.DropTable(
            name: "DetalleReservaVuelos");

        migrationBuilder.DropTable(
            name: "Hoteles");

        migrationBuilder.DropTable(
            name: "ReservaHoteles");

        migrationBuilder.DropTable(
            name: "ReservaVuelos");

        migrationBuilder.DropTable(
            name: "Vuelos");

        migrationBuilder.DropTable(
            name: "Agentes");

        migrationBuilder.DropTable(
            name: "Clientes");

        migrationBuilder.DropTable(
            name: "Aerolineas");

        migrationBuilder.DropTable(
            name: "Destinos");
    }
}
