using System;
using System.Collections.Generic;

namespace GestionPisosCompartidos.Repositories;

public partial class InquilinosVivienda
{
    public int Id { get; set; }

    public int InquilinoId { get; set; }

    public int ViviendaId { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public bool Activo { get; set; }

    public virtual Usuario Inquilino { get; set; } = null!;

    public virtual Vivienda Vivienda { get; set; } = null!;
}
