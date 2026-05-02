namespace GestionPisosCompartidos.Models.DTOs
{
    public class CrearGastoDTO
    {
        public int ViviendaId { get; set; }
        public string Concepto { get; set; } = null!;
        public string Categoria { get; set; } = null!;
        public decimal ImporteTotal { get; set; }
        public string FechaGasto { get; set; } = null!;
        public int CreadoPorId { get; set; }
        public List<int>? InquilinosIds { get; set; }
    }
}