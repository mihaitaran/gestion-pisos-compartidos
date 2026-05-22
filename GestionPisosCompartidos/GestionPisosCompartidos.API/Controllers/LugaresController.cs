using GestionPisosCompartidos.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionPisosCompartidos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LugaresController : ControllerBase
    {
        private readonly ILugaresService _service;

        public LugaresController(ILugaresService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<LugarCercanoDTO>>> BuscarCercanos(
            [FromQuery] double latitud,
            [FromQuery] double longitud,
            [FromQuery] string tipo = "supermarket",
            [FromQuery] int radio = 1500)
        {
            var resultado = await _service.BuscarLugaresCercanosAsync(latitud, longitud, tipo, radio);
            return Ok(resultado);
        }

        [HttpGet("geocodificar")]
        public async Task<ActionResult> Geocodificar([FromQuery] string direccion)
        {
            var resultado = await _service.GeocodificarDireccionAsync(direccion);
            if (resultado == null) return NotFound(new { message = "No se encontró la dirección" });
            return Ok(new { latitud = resultado.Value.lat, longitud = resultado.Value.lng });
        }

        [HttpGet("autocompletar")]
        public async Task<ActionResult> Autocompletar([FromQuery] string direccion, [FromQuery] string? ciudad = null)
        {
            var resultado = await _service.AutocompletarDireccionAsync(direccion, ciudad);
            return Ok(resultado);
        }
    }
}