using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionPisosCompartidos.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class EliminarUniqueInquilinoVivienda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InquilinosViviendas_InquilinoId_ViviendaId_FechaInicio",
                table: "InquilinosViviendas");

            migrationBuilder.CreateIndex(
                name: "IX_InquilinosViviendas_InquilinoId_ViviendaId_FechaInicio",
                table: "InquilinosViviendas",
                columns: new[] { "InquilinoId", "ViviendaId", "FechaInicio" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InquilinosViviendas_InquilinoId_ViviendaId_FechaInicio",
                table: "InquilinosViviendas");

            migrationBuilder.CreateIndex(
                name: "IX_InquilinosViviendas_InquilinoId_ViviendaId_FechaInicio",
                table: "InquilinosViviendas",
                columns: new[] { "InquilinoId", "ViviendaId", "FechaInicio" },
                unique: true);
        }
    }
}
