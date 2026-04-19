using System;
using System.Collections.Generic;

namespace GestionPisosCompartidos.Models.Entities;

public partial class Incidencia
{
    public int Id { get; set; }

    public int ViviendaId { get; set; }

    public int ReportadaPorId { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Prioridad { get; set; } = null!;

    public string? Estado { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaResolucion { get; set; }

    public virtual Usuario? ReportadaPor { get; set; } = null!;

    public virtual Vivienda? Vivienda { get; set; } = null!;
}
