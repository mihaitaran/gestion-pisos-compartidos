using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionPisosCompartidos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class IncidenciasController : ControllerBase
    {
        private readonly IIncidenciaService _service;

        public IncidenciasController(IIncidenciaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Incidencia>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Incidencia>> GetById(int id)
        {
            var incidencia = await _service.GetByIdAsync(id);
            if (incidencia == null) return NotFound();
            return Ok(incidencia);
        }

        [HttpGet("vivienda/{viviendaId}")]
        public async Task<ActionResult<List<Incidencia>>> GetByVivienda(int viviendaId)
        {
            return Ok(await _service.GetByViviendaIdAsync(viviendaId));
        }

        [HttpPost]
        public async Task<ActionResult<Incidencia>> Create(Incidencia incidencia)
        {
            var created = await _service.CreateAsync(incidencia);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Incidencia>> Update(int id, Incidencia incidencia)
        {
            incidencia.Id = id;
            var updated = await _service.UpdateAsync(incidencia);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpPatch("{id}/estado")]
        public async Task<ActionResult<Incidencia>> UpdateEstado(int id, [FromBody] string estado)
        {
            var updated = await _service.UpdateEstadoAsync(id, estado);
            if (updated == null) return NotFound();
            return Ok(updated);
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