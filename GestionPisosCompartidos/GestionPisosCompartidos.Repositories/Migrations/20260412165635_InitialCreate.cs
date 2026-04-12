using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionPisosCompartidos.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Rol = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.CheckConstraint("CK_Usuarios_Rol", "Rol IN ('Propietario', 'Inquilino')");
                });

            migrationBuilder.CreateTable(
                name: "Viviendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Direccion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CodigoPostal = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NumHabitaciones = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    PropietarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viviendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Viviendas_Usuarios_PropietarioId",
                        column: x => x.PropietarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Gastos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViviendaId = table.Column<int>(type: "int", nullable: false),
                    Concepto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImporteTotal = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    FechaGasto = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreadoPorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gastos", x => x.Id);
                    table.CheckConstraint("CK_Gastos_Categoria", "Categoria IN ('Luz', 'Agua', 'Gas', 'Internet', 'Comunidad', 'Alquiler', 'Otro')");
                    table.CheckConstraint("CK_Gastos_ImporteTotal", "ImporteTotal > 0");
                    table.ForeignKey(
                        name: "FK_Gastos_Usuarios_CreadoPorId",
                        column: x => x.CreadoPorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Gastos_Viviendas_ViviendaId",
                        column: x => x.ViviendaId,
                        principalTable: "Viviendas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Incidencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViviendaId = table.Column<int>(type: "int", nullable: false),
                    ReportadaPorId = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Prioridad = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Media"),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Abierta"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    FechaResolucion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidencias", x => x.Id);
                    table.CheckConstraint("CK_Incidencias_Estado", "Estado IN ('Abierta', 'EnProceso', 'Resuelta', 'Cerrada')");
                    table.CheckConstraint("CK_Incidencias_Prioridad", "Prioridad IN ('Baja', 'Media', 'Alta', 'Urgente')");
                    table.ForeignKey(
                        name: "FK_Incidencias_Usuarios_ReportadaPorId",
                        column: x => x.ReportadaPorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Incidencias_Viviendas_ViviendaId",
                        column: x => x.ViviendaId,
                        principalTable: "Viviendas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InquilinosViviendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InquilinoId = table.Column<int>(type: "int", nullable: false),
                    ViviendaId = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaFin = table.Column<DateOnly>(type: "date", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquilinosViviendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InquilinosViviendas_Usuarios_InquilinoId",
                        column: x => x.InquilinoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InquilinosViviendas_Viviendas_ViviendaId",
                        column: x => x.ViviendaId,
                        principalTable: "Viviendas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TareasCalendario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViviendaId = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FechaProgramada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AsignadaAId = table.Column<int>(type: "int", nullable: true),
                    CreadaPorId = table.Column<int>(type: "int", nullable: false),
                    Completada = table.Column<bool>(type: "bit", nullable: false),
                    Recurrente = table.Column<bool>(type: "bit", nullable: false),
                    FrecuenciaDias = table.Column<int>(type: "int", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareasCalendario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TareasCalendario_Usuarios_AsignadaAId",
                        column: x => x.AsignadaAId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TareasCalendario_Usuarios_CreadaPorId",
                        column: x => x.CreadaPorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TareasCalendario_Viviendas_ViviendaId",
                        column: x => x.ViviendaId,
                        principalTable: "Viviendas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GastoId = table.Column<int>(type: "int", nullable: false),
                    InquilinoId = table.Column<int>(type: "int", nullable: false),
                    Importe = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Pendiente"),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.Id);
                    table.CheckConstraint("CK_Pagos_Estado", "Estado IN ('Pendiente', 'Pagado', 'Rechazado')");
                    table.CheckConstraint("CK_Pagos_Importe", "Importe > 0");
                    table.ForeignKey(
                        name: "FK_Pagos_Gastos_GastoId",
                        column: x => x.GastoId,
                        principalTable: "Gastos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pagos_Usuarios_InquilinoId",
                        column: x => x.InquilinoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_CreadoPorId",
                table: "Gastos",
                column: "CreadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_ViviendaId",
                table: "Gastos",
                column: "ViviendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_ReportadaPorId",
                table: "Incidencias",
                column: "ReportadaPorId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_ViviendaId",
                table: "Incidencias",
                column: "ViviendaId");

            migrationBuilder.CreateIndex(
                name: "IX_InquilinosViviendas_InquilinoId",
                table: "InquilinosViviendas",
                column: "InquilinoId");

            migrationBuilder.CreateIndex(
                name: "IX_InquilinosViviendas_InquilinoId_ViviendaId_FechaInicio",
                table: "InquilinosViviendas",
                columns: new[] { "InquilinoId", "ViviendaId", "FechaInicio" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InquilinosViviendas_ViviendaId",
                table: "InquilinosViviendas",
                column: "ViviendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_GastoId",
                table: "Pagos",
                column: "GastoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_InquilinoId",
                table: "Pagos",
                column: "InquilinoId");

            migrationBuilder.CreateIndex(
                name: "IX_TareasCalendario_AsignadaAId",
                table: "TareasCalendario",
                column: "AsignadaAId");

            migrationBuilder.CreateIndex(
                name: "IX_TareasCalendario_CreadaPorId",
                table: "TareasCalendario",
                column: "CreadaPorId");

            migrationBuilder.CreateIndex(
                name: "IX_TareasCalendario_ViviendaId",
                table: "TareasCalendario",
                column: "ViviendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Viviendas_PropietarioId",
                table: "Viviendas",
                column: "PropietarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Incidencias");

            migrationBuilder.DropTable(
                name: "InquilinosViviendas");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "TareasCalendario");

            migrationBuilder.DropTable(
                name: "Gastos");

            migrationBuilder.DropTable(
                name: "Viviendas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
