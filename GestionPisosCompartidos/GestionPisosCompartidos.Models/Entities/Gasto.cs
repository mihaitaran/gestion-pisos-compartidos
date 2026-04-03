using System;
using System.Collections.Generic;

namespace GestionPisosCompartidos.Repositories;

public partial class Gasto
{
    public int Id { get; set; }

    public int ViviendaId { get; set; }

    public string Concepto { get; set; } = null!;

    public string Categoria { get; set; } = null!;

    public decimal ImporteTotal { get; set; }

    public DateOnly FechaGasto { get; set; }

    public DateTime FechaRegistro { get; set; }

    public int CreadoPorId { get; set; }

    public virtual Usuario CreadoPor { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual Vivienda Vivienda { get; set; } = null!;
}
