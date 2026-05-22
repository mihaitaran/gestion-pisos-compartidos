namespace GestionPisosCompartidos.Client.DTOs
{
    public class LugarDTO
    {
        public string Nombre { get; set; } = "";
        public string Direccion { get; set; } = "";
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string Tipo { get; set; } = "";
    }
}