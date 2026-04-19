namespace GestionPisosCompartidos.Client.DTOs
{
    public class PagoDTO
    {
        public int Id { get; set; }
        public int GastoId { get; set; }
        public int InquilinoId { get; set; }
        public decimal Importe { get; set; }
        public string Estado { get; set; } = "";
        public string? FechaPago { get; set; }
        public GastoDTO? Gasto { get; set; }
        public UsuarioDTO? Inquilino { get; set; }
    }
}