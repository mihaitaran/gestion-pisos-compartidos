using System;
using System.Collections.Generic;

namespace GestionPisosCompartidos.Models.Entities;

public partial class Pago
{
    public int Id { get; set; }

    public int GastoId { get; set; }

    public int InquilinoId { get; set; }

    public decimal Importe { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime? FechaPago { get; set; }

    public virtual Gasto Gasto { get; set; } = null!;

    public virtual Usuario Inquilino { get; set; } = null!;
}
