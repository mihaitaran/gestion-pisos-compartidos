namespace GestionPisosCompartidos.Client.DTOs
{
    public class InquilinoViviendaDTO
    {
        public int Id { get; set; }
        public int InquilinoId { get; set; }
        public int ViviendaId { get; set; }
        public string FechaInicio { get; set; } = "";
        public string? FechaFin { get; set; }
        public bool Activo { get; set; }
        public UsuarioDTO? Inquilino { get; set; }
        public ViviendaDTO? Vivienda { get; set; }
    }
}