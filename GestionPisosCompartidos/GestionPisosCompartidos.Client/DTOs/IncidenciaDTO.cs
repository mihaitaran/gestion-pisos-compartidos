namespace GestionPisosCompartidos.Client.DTOs
{
    public class IncidenciaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string Prioridad { get; set; } = "";
        public string? Estado { get; set; }
        public int ViviendaId { get; set; }
        public string FechaCreacion { get; set; } = "";
        public ViviendaDTO? Vivienda { get; set; }
        public UsuarioDTO? ReportadaPor { get; set; }
    }
}