using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionPisosCompartidos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HabitacionesController : ControllerBase
    {
        private readonly IHabitacionService _service;

        public HabitacionesController(IHabitacionService service)
        {
            _service = service;
        }

        [HttpGet("vivienda/{viviendaId}")]
        public async Task<ActionResult<List<Habitacion>>> GetByVivienda(int viviendaId)
        {
            return Ok(await _service.GetByViviendaIdAsync(viviendaId));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Habitacion>> GetById(int id)
        {
            var habitacion = await _service.GetByIdAsync(id);
            if (habitacion == null) return NotFound();
            return Ok(habitacion);
        }

        [HttpPost]
        public async Task<ActionResult<Habitacion>> Create(Habitacion habitacion)
        {
            try
            {
                var created = await _service.CreateAsync(habitacion);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Habitacion>> Update(int id, Habitacion habitacion)
        {
            habitacion.Id = id;
            var updated = await _service.UpdateAsync(habitacion);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted) return NotFound();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}