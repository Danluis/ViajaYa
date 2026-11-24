using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViajaYa.Migrations;

/// <inheritdoc />
public partial class _20261122142220_AjustesFinalesModelos : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Fecha",
            table: "ReservaVuelos");

        migrationBuilder.DropColumn(
            name: "Observacion",
            table: "ReservaVuelos");

        migrationBuilder.DropColumn(
            name: "FechaEntrada",
            table: "ReservaHoteles");

        migrationBuilder.DropColumn(
            name: "FechaSalida",
            table: "ReservaHoteles");

        migrationBuilder.DropColumn(
            name: "Observacion",
            table: "ReservaHoteles");

        migrationBuilder.RenameColumn(
            name: "Total",
            table: "ReservaVuelos",
            newName: "FechaReserva");

        migrationBuilder.RenameColumn(
            name: "Total",
            table: "ReservaHoteles",
            newName: "FechaReserva");

        migrationBuilder.RenameColumn(
            name: "Precio",
            table: "DetalleReservaVuelos",
            newName: "PrecioUnitario");

        migrationBuilder.RenameColumn(
            name: "Cantidad",
            table: "DetalleReservaVuelos",
            newName: "CantidadAsientos");

        migrationBuilder.RenameColumn(
            name: "Precio",
            table: "DetalleReservaHoteles",
            newName: "PrecioUnitario");

        migrationBuilder.RenameColumn(
            name: "Cantidad",
            table: "DetalleReservaHoteles",
            newName: "CantidadNoches");

        migrationBuilder.AddColumn<string>(
            name: "Estado",
            table: "ReservaVuelos",
            type: "TEXT",
            maxLength: 20,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "Estado",
            table: "ReservaHoteles",
            type: "TEXT",
            maxLength: 20,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<int>(
            name: "Categoria",
            table: "Hoteles",
            type: "INTEGER",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<decimal>(
            name: "PrecioPorNoche",
            table: "Hoteles",
            type: "TEXT",
            nullable: false,
            defaultValue: 0m);

        migrationBuilder.AddColumn<int>(
            name: "CantidadHabitaciones",
            table: "DetalleReservaHoteles",
            type: "INTEGER",
            nullable: false,
            defaultValue: 0);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Estado",
            table: "ReservaVuelos");

        migrationBuilder.DropColumn(
            name: "Estado",
            table: "ReservaHoteles");

        migrationBuilder.DropColumn(
            name: "Categoria",
            table: "Hoteles");

        migrationBuilder.DropColumn(
            name: "PrecioPorNoche",
            table: "Hoteles");

        migrationBuilder.DropColumn(
            name: "CantidadHabitaciones",
            table: "DetalleReservaHoteles");

        migrationBuilder.RenameColumn(
            name: "FechaReserva",
            table: "ReservaVuelos",
            newName: "Total");

        migrationBuilder.RenameColumn(
            name: "FechaReserva",
            table: "ReservaHoteles",
            newName: "Total");

        migrationBuilder.RenameColumn(
            name: "PrecioUnitario",
            table: "DetalleReservaVuelos",
            newName: "Precio");

        migrationBuilder.RenameColumn(
            name: "CantidadAsientos",
            table: "DetalleReservaVuelos",
            newName: "Cantidad");

        migrationBuilder.RenameColumn(
            name: "PrecioUnitario",
            table: "DetalleReservaHoteles",
            newName: "Precio");

        migrationBuilder.RenameColumn(
            name: "CantidadNoches",
            table: "DetalleReservaHoteles",
            newName: "Cantidad");

        migrationBuilder.AddColumn<DateTime>(
            name: "Fecha",
            table: "ReservaVuelos",
            type: "TEXT",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<string>(
            name: "Observacion",
            table: "ReservaVuelos",
            type: "TEXT",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<DateTime>(
            name: "FechaEntrada",
            table: "ReservaHoteles",
            type: "TEXT",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "FechaSalida",
            table: "ReservaHoteles",
            type: "TEXT",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<string>(
            name: "Observacion",
            table: "ReservaHoteles",
            type: "TEXT",
            nullable: false,
            defaultValue: "");
    }
}
