namespace GestionPisosCompartidos.Client.DTOs
{
    public class MensajeDTO
    {
        public int Id { get; set; }
        public int ViviendaId { get; set; }
        public int EmisorId { get; set; }
        public string Contenido { get; set; } = "";
        public string FechaEnvio { get; set; } = "";
        public UsuarioDTO? Emisor { get; set; }
    }
}