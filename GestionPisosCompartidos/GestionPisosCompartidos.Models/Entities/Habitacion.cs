namespace GestionPisosCompartidos.Models.Entities;

public class Habitacion
{
    public int Id { get; set; }
    public int ViviendaId { get; set; }
    public string Numero { get; set; } = null!;
    public bool TieneBano { get; set; }
    public string? Descripcion { get; set; }
    public decimal? PrecioMensual { get; set; }

    public virtual Vivienda? Vivienda { get; set; }
    public virtual ICollection<InquilinosVivienda> InquilinosVivienda { get; set; } = new List<InquilinosVivienda>();
}
