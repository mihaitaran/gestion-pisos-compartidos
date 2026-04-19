namespace GestionPisosCompartidos.Client.DTOs
{
    public class TareaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = "";
        public string? Descripcion { get; set; }
        public string FechaProgramada { get; set; } = "";
        public bool Completada { get; set; }
        public bool Recurrente { get; set; }
        public int? FrecuenciaDias { get; set; }
        public ViviendaDTO? Vivienda { get; set; }
        public UsuarioDTO? AsignadaA { get; set; }
        public UsuarioDTO? CreadaPor { get; set; }
    }
}