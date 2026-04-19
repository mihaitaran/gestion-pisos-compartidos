namespace GestionPisosCompartidos.Client.DTOs
{
    public class GastoDTO
    {
        public int Id { get; set; }
        public string Concepto { get; set; } = "";
        public string Categoria { get; set; } = "";
        public decimal ImporteTotal { get; set; }
        public string FechaGasto { get; set; } = "";
        public int ViviendaId { get; set; }
        public int CreadoPorId { get; set; }
        public ViviendaDTO? Vivienda { get; set; }
        public UsuarioDTO? CreadoPor { get; set; }
        public List<PagoDTO> Pagos { get; set; } = new();
    }
}