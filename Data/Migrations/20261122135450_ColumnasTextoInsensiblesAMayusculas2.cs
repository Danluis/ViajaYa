using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViajaYa.Migrations;

/// <inheritdoc />
public partial class _20261122135450_ColumnasTextoInsensiblesAMayusculas2 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "NumeroVuelo",
            table: "Vuelos",
            type: "TEXT",
            maxLength: 20,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 20,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "Observacion",
            table: "ReservaVuelos",
            type: "TEXT",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "Observacion",
            table: "ReservaHoteles",
            type: "TEXT",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "Nombre",
            table: "Hoteles",
            type: "TEXT",
            maxLength: 100,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 100,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "Direccion",
            table: "Hoteles",
            type: "TEXT",
            maxLength: 150,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 150,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "Pais",
            table: "Destinos",
            type: "TEXT",
            maxLength: 60,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 60,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "Descripcion",
            table: "Destinos",
            type: "TEXT",
            maxLength: 300,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 300,
            oldNullable: true,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "Ciudad",
            table: "Destinos",
            type: "TEXT",
            maxLength: 80,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 80,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "Telefono",
            table: "Clientes",
            type: "TEXT",
            maxLength: 20,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 20,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "Nombre",
            table: "Clientes",
            type: "TEXT",
            maxLength: 100,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 100,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "Email",
            table: "Clientes",
            type: "TEXT",
            maxLength: 100,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 100,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "Cedula",
            table: "Clientes",
            type: "TEXT",
            maxLength: 20,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 20,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "Value",
            table: "AspNetUserTokens",
            type: "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "AspNetUserTokens",
            type: "TEXT",
            maxLength: 128,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 128,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "LoginProvider",
            table: "AspNetUserTokens",
            type: "TEXT",
            maxLength: 128,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 128,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "UserId",
            table: "AspNetUserTokens",
            type: "TEXT",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "UserName",
            table: "AspNetUsers",
            type: "TEXT",
            maxLength: 256,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 256,
            oldNullable: true,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "SecurityStamp",
            table: "AspNetUsers",
            type: "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "PhoneNumber",
            table: "AspNetUsers",
            type: "TEXT",
            maxLength: 256,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 256,
            oldNullable: true,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "PasswordHash",
            table: "AspNetUsers",
            type: "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "NormalizedUserName",
            table: "AspNetUsers",
            type: "TEXT",
            maxLength: 256,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 256,
            oldNullable: true,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "NormalizedEmail",
            table: "AspNetUsers",
            type: "TEXT",
            maxLength: 256,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 256,
            oldNullable: true,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "Email",
            table: "AspNetUsers",
            type: "TEXT",
            maxLength: 256,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 256,
            oldNullable: true,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "ConcurrencyStamp",
            table: "AspNetUsers",
            type: "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "Id",
            table: "AspNetUsers",
            type: "TEXT",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "RoleId",
            table: "AspNetUserRoles",
            type: "TEXT",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "UserId",
            table: "AspNetUserRoles",
            type: "TEXT",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "UserId",
            table: "AspNetUserPasskeys",
            type: "TEXT",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "UserId",
            table: "AspNetUserLogins",
            type: "TEXT",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "ProviderDisplayName",
            table: "AspNetUserLogins",
            type: "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "ProviderKey",
            table: "AspNetUserLogins",
            type: "TEXT",
            maxLength: 128,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 128,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "LoginProvider",
            table: "AspNetUserLogins",
            type: "TEXT",
            maxLength: 128,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 128,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "UserId",
            table: "AspNetUserClaims",
            type: "TEXT",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "ClaimValue",
            table: "AspNetUserClaims",
            type: "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "ClaimType",
            table: "AspNetUserClaims",
            type: "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "NormalizedName",
            table: "AspNetRoles",
            type: "TEXT",
            maxLength: 256,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 256,
            oldNullable: true,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "AspNetRoles",
            type: "TEXT",
            maxLength: 256,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 256,
            oldNullable: true,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "ConcurrencyStamp",
            table: "AspNetRoles",
            type: "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "Id",
            table: "AspNetRoles",
            type: "TEXT",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "RoleId",
            table: "AspNetRoleClaims",
            type: "TEXT",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "ClaimValue",
            table: "AspNetRoleClaims",
            type: "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "ClaimType",
            table: "AspNetRoleClaims",
            type: "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "Nombre",
            table: "Agentes",
            type: "TEXT",
            maxLength: 100,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 100,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "Codigo",
            table: "Agentes",
            type: "TEXT",
            maxLength: 20,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 20,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "Pais",
            table: "Aerolineas",
            type: "TEXT",
            maxLength: 60,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 60,
            oldCollation: "NOCASE");

        migrationBuilder.AlterColumn<string>(
            name: "Nombre",
            table: "Aerolineas",
            type: "TEXT",
            maxLength: 100,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 100,
            oldCollation: "NOCASE");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "NumeroVuelo",
            table: "Vuelos",
            type: "TEXT",
            maxLength: 20,
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 20);

        migrationBuilder.AlterColumn<string>(
            name: "Observacion",
            table: "ReservaVuelos",
            type: "TEXT",
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            name: "Observacion",
            table: "ReservaHoteles",
            type: "TEXT",
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            name: "Nombre",
            table: "Hoteles",
            type: "TEXT",
            maxLength: 100,
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<string>(
            name: "Direccion",
            table: "Hoteles",
            type: "TEXT",
            maxLength: 150,
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 150);

        migrationBuilder.AlterColumn<string>(
            name: "Pais",
            table: "Destinos",
            type: "TEXT",
            maxLength: 60,
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 60);

        migrationBuilder.AlterColumn<string>(
            name: "Descripcion",
            table: "Destinos",
            type: "TEXT",
            maxLength: 300,
            nullable: true,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 300,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Ciudad",
            table: "Destinos",
            type: "TEXT",
            maxLength: 80,
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 80);

        migrationBuilder.AlterColumn<string>(
            name: "Telefono",
            table: "Clientes",
            type: "TEXT",
            maxLength: 20,
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 20);

        migrationBuilder.AlterColumn<string>(
            name: "Nombre",
            table: "Clientes",
            type: "TEXT",
            maxLength: 100,
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<string>(
            name: "Email",
            table: "Clientes",
            type: "TEXT",
            maxLength: 100,
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<string>(
            name: "Cedula",
            table: "Clientes",
            type: "TEXT",
            maxLength: 20,
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 20);

        migrationBuilder.AlterColumn<string>(
            name: "Value",
            table: "AspNetUserTokens",
            type: "TEXT",
            nullable: true,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "AspNetUserTokens",
            type: "TEXT",
            maxLength: 128,
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 128);

        migrationBuilder.AlterColumn<string>(
            name: "LoginProvider",
            table: "AspNetUserTokens",
            type: "TEXT",
            maxLength: 128,
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 128);

        migrationBuilder.AlterColumn<string>(
            name: "UserId",
            table: "AspNetUserTokens",
            type: "TEXT",
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            name: "UserName",
            table: "AspNetUsers",
            type: "TEXT",
            maxLength: 256,
            nullable: true,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 256,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "SecurityStamp",
            table: "AspNetUsers",
            type: "TEXT",
            nullable: true,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "PhoneNumber",
            table: "AspNetUsers",
            type: "TEXT",
            maxLength: 256,
            nullable: true,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 256,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "PasswordHash",
            table: "AspNetUsers",
            type: "TEXT",
            nullable: true,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "NormalizedUserName",
            table: "AspNetUsers",
            type: "TEXT",
            maxLength: 256,
            nullable: true,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 256,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "NormalizedEmail",
            table: "AspNetUsers",
            type: "TEXT",
            maxLength: 256,
            nullable: true,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 256,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Email",
            table: "AspNetUsers",
            type: "TEXT",
            maxLength: 256,
            nullable: true,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 256,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "ConcurrencyStamp",
            table: "AspNetUsers",
            type: "TEXT",
            nullable: true,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Id",
            table: "AspNetUsers",
            type: "TEXT",
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            name: "RoleId",
            table: "AspNetUserRoles",
            type: "TEXT",
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            name: "UserId",
            table: "AspNetUserRoles",
            type: "TEXT",
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            name: "UserId",
            table: "AspNetUserPasskeys",
            type: "TEXT",
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            name: "UserId",
            table: "AspNetUserLogins",
            type: "TEXT",
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            name: "ProviderDisplayName",
            table: "AspNetUserLogins",
            type: "TEXT",
            nullable: true,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "ProviderKey",
            table: "AspNetUserLogins",
            type: "TEXT",
            maxLength: 128,
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 128);

        migrationBuilder.AlterColumn<string>(
            name: "LoginProvider",
            table: "AspNetUserLogins",
            type: "TEXT",
            maxLength: 128,
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 128);

        migrationBuilder.AlterColumn<string>(
            name: "UserId",
            table: "AspNetUserClaims",
            type: "TEXT",
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            name: "ClaimValue",
            table: "AspNetUserClaims",
            type: "TEXT",
            nullable: true,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "ClaimType",
            table: "AspNetUserClaims",
            type: "TEXT",
            nullable: true,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "NormalizedName",
            table: "AspNetRoles",
            type: "TEXT",
            maxLength: 256,
            nullable: true,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 256,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "AspNetRoles",
            type: "TEXT",
            maxLength: 256,
            nullable: true,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 256,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "ConcurrencyStamp",
            table: "AspNetRoles",
            type: "TEXT",
            nullable: true,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Id",
            table: "AspNetRoles",
            type: "TEXT",
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            name: "RoleId",
            table: "AspNetRoleClaims",
            type: "TEXT",
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            name: "ClaimValue",
            table: "AspNetRoleClaims",
            type: "TEXT",
            nullable: true,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "ClaimType",
            table: "AspNetRoleClaims",
            type: "TEXT",
            nullable: true,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Nombre",
            table: "Agentes",
            type: "TEXT",
            maxLength: 100,
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<string>(
            name: "Codigo",
            table: "Agentes",
            type: "TEXT",
            maxLength: 20,
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 20);

        migrationBuilder.AlterColumn<string>(
            name: "Pais",
            table: "Aerolineas",
            type: "TEXT",
            maxLength: 60,
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 60);

        migrationBuilder.AlterColumn<string>(
            name: "Nombre",
            table: "Aerolineas",
            type: "TEXT",
            maxLength: 100,
            nullable: false,
            collation: "NOCASE",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 100);
    }
}
