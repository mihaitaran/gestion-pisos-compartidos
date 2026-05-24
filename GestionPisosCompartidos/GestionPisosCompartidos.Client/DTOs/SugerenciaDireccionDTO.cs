namespace GestionPisosCompartidos.Client.DTOs
{
    public class SugerenciaDireccionDTO
    {
        public string Nombre { get; set; } = "";
        public string Calle { get; set; } = "";
        public string? Numero { get; set; }
        public string? Ciudad { get; set; }
        public string? CodigoPostal { get; set; }
    }
}