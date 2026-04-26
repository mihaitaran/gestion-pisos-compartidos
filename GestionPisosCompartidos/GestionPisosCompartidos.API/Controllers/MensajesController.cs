using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionPisosCompartidos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MensajesController : ControllerBase
    {
        private readonly IMensajeService _service;

        public MensajesController(IMensajeService service)
        {
            _service = service;
        }

        [HttpGet("vivienda/{viviendaId}")]
        public async Task<ActionResult<List<Mensaje>>> GetByVivienda(int viviendaId)
        {
            return Ok(await _service.GetByViviendaIdAsync(viviendaId));
        }

        [HttpPost]
        public async Task<ActionResult<Mensaje>> Create(Mensaje mensaje)
        {
            var created = await _service.CreateAsync(mensaje);
            return Ok(created);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}