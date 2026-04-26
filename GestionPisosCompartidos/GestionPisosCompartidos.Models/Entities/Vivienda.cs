using System;
using System.Collections.Generic;

namespace GestionPisosCompartidos.Models.Entities;

public partial class Vivienda
{
    public int Id { get; set; }

    public string Direccion { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public string CodigoPostal { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int NumHabitaciones { get; set; }

    public int PropietarioId { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<Habitacion> Habitaciones { get; set; } = new List<Habitacion>();

    public virtual ICollection<Mensaje> Mensajes { get; set; } = new List<Mensaje>();

    public virtual ICollection<Gasto> Gastos { get; set; } = new List<Gasto>();

    public virtual ICollection<Incidencia> Incidencia { get; set; } = new List<Incidencia>();

    public virtual ICollection<InquilinosVivienda> InquilinosVivienda { get; set; } = new List<InquilinosVivienda>();

    public virtual Usuario? Propietario { get; set; }

    public virtual ICollection<TareasCalendario> TareasCalendarios { get; set; } = new List<TareasCalendario>();
}
