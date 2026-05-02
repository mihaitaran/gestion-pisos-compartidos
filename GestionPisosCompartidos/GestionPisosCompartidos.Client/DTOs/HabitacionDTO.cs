namespace GestionPisosCompartidos.Client.DTOs
{
    public class HabitacionDTO
    {
        public int Id { get; set; }
        public int ViviendaId { get; set; }
        public string Numero { get; set; } = "";
        public bool TieneBano { get; set; }
        public string? Descripcion { get; set; }
        public decimal? PrecioMensual { get; set; }
        public ViviendaDTO? Vivienda { get; set; }
    }
}