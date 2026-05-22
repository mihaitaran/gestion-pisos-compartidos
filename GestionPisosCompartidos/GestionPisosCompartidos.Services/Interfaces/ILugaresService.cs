namespace GestionPisosCompartidos.Services.Interfaces
{
    public interface ILugaresService
    {
        Task<List<LugarCercanoDTO>> BuscarLugaresCercanosAsync(double latitud, double longitud, string tipo, int radio = 1500);
        Task<(double lat, double lng)?> GeocodificarDireccionAsync(string direccion);

        Task<List<SugerenciaDireccionDTO>> AutocompletarDireccionAsync(string direccion, string? ciudad = null);
    }

    public class LugarCercanoDTO
    {
        public string Nombre { get; set; } = "";
        public string Direccion { get; set; } = "";
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string Tipo { get; set; } = "";
    }

    public class SugerenciaDireccionDTO
    {
        public string Nombre { get; set; } = "";
        public string Calle { get; set; } = "";
        public string? Numero { get; set; }
        public string? Ciudad { get; set; }
        public string? CodigoPostal { get; set; }
    }
}