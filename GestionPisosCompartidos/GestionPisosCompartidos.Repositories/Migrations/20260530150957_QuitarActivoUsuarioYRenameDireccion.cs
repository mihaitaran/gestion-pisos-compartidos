using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionPisosCompartidos.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class QuitarActivoUsuarioYRenameDireccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "Direccion",
                table: "Viviendas",
                newName: "Calle");

            migrationBuilder.AlterColumn<bool>(
                name: "Activo",
                table: "InquilinosViviendas",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Calle",
                table: "Viviendas",
                newName: "Direccion");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Activo",
                table: "InquilinosViviendas",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
