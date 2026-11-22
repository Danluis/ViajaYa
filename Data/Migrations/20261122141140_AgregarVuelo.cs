using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViajaYa.Migrations;

/// <inheritdoc />
public partial class _20261122141140_AgregarVuelo : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "Fecha",
            table: "Vuelos",
            newName: "FechaSalida");

        migrationBuilder.AddColumn<DateTime>(
            name: "FechaLlegada",
            table: "Vuelos",
            type: "TEXT",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "FechaLlegada",
            table: "Vuelos");

        migrationBuilder.RenameColumn(
            name: "FechaSalida",
            table: "Vuelos",
            newName: "Fecha");
    }
}
