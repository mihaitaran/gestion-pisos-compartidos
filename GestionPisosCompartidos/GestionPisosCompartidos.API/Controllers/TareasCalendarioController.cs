using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionPisosCompartidos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TareasCalendarioController : ControllerBase
    {
        private readonly ITareaCalendarioService _service;

        public TareasCalendarioController(ITareaCalendarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<TareasCalendario>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TareasCalendario>> GetById(int id)
        {
            var tarea = await _service.GetByIdAsync(id);
            if (tarea == null) return NotFound();
            return Ok(tarea);
        }

        [HttpGet("vivienda/{viviendaId}")]
        public async Task<ActionResult<List<TareasCalendario>>> GetByVivienda(int viviendaId)
        {
            return Ok(await _service.GetByViviendaIdAsync(viviendaId));
        }

        [HttpPost]
        public async Task<ActionResult<TareasCalendario>> Create(TareasCalendario tarea)
        {
            var created = await _service.CreateAsync(tarea);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TareasCalendario>> Update(int id, TareasCalendario tarea)
        {
            tarea.Id = id;
            var updated = await _service.UpdateAsync(tarea);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpPatch("{id}/completar")]
        public async Task<ActionResult<TareasCalendario>> Completar(int id)
        {
            var completed = await _service.CompletarAsync(id);
            if (completed == null) return NotFound();
            return Ok(completed);
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