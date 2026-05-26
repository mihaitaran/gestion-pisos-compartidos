using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionPisosCompartidos.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class QuitarHabitacionesYEscalera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InquilinosViviendas_Habitaciones_HabitacionId",
                table: "InquilinosViviendas");

            migrationBuilder.DropTable(
                name: "Habitaciones");

            migrationBuilder.DropIndex(
                name: "IX_InquilinosViviendas_HabitacionId",
                table: "InquilinosViviendas");

            migrationBuilder.DropColumn(
                name: "Escalera",
                table: "Viviendas");

            migrationBuilder.DropColumn(
                name: "HabitacionId",
                table: "InquilinosViviendas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Escalera",
                table: "Viviendas",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HabitacionId",
                table: "InquilinosViviendas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Habitaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViviendaId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PrecioMensual = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    TieneBano = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habitaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Habitaciones_Viviendas_ViviendaId",
                        column: x => x.ViviendaId,
                        principalTable: "Viviendas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InquilinosViviendas_HabitacionId",
                table: "InquilinosViviendas",
                column: "HabitacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Habitaciones_ViviendaId",
                table: "Habitaciones",
                column: "ViviendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_InquilinosViviendas_Habitaciones_HabitacionId",
                table: "InquilinosViviendas",
                column: "HabitacionId",
                principalTable: "Habitaciones",
                principalColumn: "Id");
        }
    }
}
