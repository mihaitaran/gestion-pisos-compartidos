using System;
using System.Collections.Generic;

namespace GestionPisosCompartidos.Models.Entities;

public partial class TareasCalendario
{
    public int Id { get; set; }

    public int ViviendaId { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime FechaProgramada { get; set; }

    public int? AsignadaAid { get; set; }

    public int CreadaPorId { get; set; }

    public bool Completada { get; set; }

    public bool Recurrente { get; set; }

    public int? FrecuenciaDias { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual Usuario? AsignadaA { get; set; }

    public virtual Usuario? CreadaPor { get; set; } = null!;

    public virtual Vivienda? Vivienda { get; set; } = null!;
}
