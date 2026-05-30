using GestionPisosCompartidos.Models.DTOs;

namespace GestionPisosCompartidos.Services.Interfaces
{
    public interface ILugaresService
    {
        Task<List<LugarCercanoDTO>> BuscarLugaresCercanosAsync(double latitud, double longitud, string tipo, int radio = 1500);
        Task<(double lat, double lng)?> GeocodificarDireccionAsync(string direccion);

        Task<List<SugerenciaDireccionDTO>> AutocompletarDireccionAsync(string direccion, string? ciudad = null);
    }
}