using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionPisosCompartidos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PagosController : ControllerBase
    {
        private readonly IPagoService _service;

        public PagosController(IPagoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pago>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pago>> GetById(int id)
        {
            var pago = await _service.GetByIdAsync(id);
            if (pago == null) return NotFound();
            return Ok(pago);
        }

        [HttpGet("inquilino/{inquilinoId}")]
        public async Task<ActionResult<List<Pago>>> GetByInquilino(int inquilinoId)
        {
            return Ok(await _service.GetByInquilinoIdAsync(inquilinoId));
        }

        [HttpGet("gasto/{gastoId}")]
        public async Task<ActionResult<List<Pago>>> GetByGasto(int gastoId)
        {
            return Ok(await _service.GetByGastoIdAsync(gastoId));
        }

        [HttpPost]
        public async Task<ActionResult<Pago>> Create(Pago pago)
        {
            var created = await _service.CreateAsync(pago);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPatch("{id}/estado")]
        public async Task<ActionResult<Pago>> UpdateEstado(int id, [FromBody] string estado)
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