namespace GestionPisosCompartidos.Models.Entities;

public class Mensaje
{
    public int Id { get; set; }
    public int ViviendaId { get; set; }
    public int EmisorId { get; set; }
    public string Contenido { get; set; } = null!;
    public DateTime FechaEnvio { get; set; }

    public virtual Vivienda? Vivienda { get; set; }
    public virtual Usuario? Emisor { get; set; }
}