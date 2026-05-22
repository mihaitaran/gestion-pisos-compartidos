using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionPisosCompartidos.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AnadirCamposDireccionVivienda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumHabitaciones",
                table: "Viviendas");

            migrationBuilder.AddColumn<string>(
                name: "Escalera",
                table: "Viviendas",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Numero",
                table: "Viviendas",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Piso",
                table: "Viviendas",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Puerta",
                table: "Viviendas",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Escalera",
                table: "Viviendas");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Viviendas");

            migrationBuilder.DropColumn(
                name: "Piso",
                table: "Viviendas");

            migrationBuilder.DropColumn(
                name: "Puerta",
                table: "Viviendas");

            migrationBuilder.AddColumn<int>(
                name: "NumHabitaciones",
                table: "Viviendas",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }
    }
}
