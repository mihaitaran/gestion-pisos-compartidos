using System;
using System.Collections.Generic;

namespace GestionPisosCompartidos.Models.Entities;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Telefono { get; set; }

    public string Rol { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<Gasto> Gastos { get; set; } = new List<Gasto>();

    public virtual ICollection<Incidencia> Incidencia { get; set; } = new List<Incidencia>();

    public virtual ICollection<InquilinosVivienda> InquilinosVivienda { get; set; } = new List<InquilinosVivienda>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual ICollection<TareasCalendario> TareasCalendarioAsignadaAs { get; set; } = new List<TareasCalendario>();

    public virtual ICollection<TareasCalendario> TareasCalendarioCreadaPors { get; set; } = new List<TareasCalendario>();

    public virtual ICollection<Vivienda> Vivienda { get; set; } = new List<Vivienda>();

    public virtual ICollection<Mensaje> Mensajes { get; set; } = new List<Mensaje>();
}
