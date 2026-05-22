namespace GestionPisosCompartidos.Client.DTOs
{
    public class ViviendaDTO
    {
        public int Id { get; set; }
        public string Direccion { get; set; } = "";
        public string? Numero { get; set; }
        public string? Piso { get; set; }
        public string? Puerta { get; set; }
        public string? Escalera { get; set; }
        public string Ciudad { get; set; } = "";
        public string CodigoPostal { get; set; } = "";
        public string? Descripcion { get; set; }
        public int PropietarioId { get; set; }
    }
}