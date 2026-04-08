using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionPisosCompartidos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GastosController : ControllerBase
    {
        private readonly IGastoService _service;

        public GastosController(IGastoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Gasto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Gasto>> GetById(int id)
        {
            var gasto = await _service.GetByIdAsync(id);
            if (gasto == null) return NotFound();
            return Ok(gasto);
        }

        [HttpGet("vivienda/{viviendaId}")]
        public async Task<ActionResult<List<Gasto>>> GetByVivienda(int viviendaId)
        {
            return Ok(await _service.GetByViviendaIdAsync(viviendaId));
        }

        [HttpPost]
        public async Task<ActionResult<Gasto>> Create(Gasto gasto)
        {
            var created = await _service.CreateAsync(gasto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Gasto>> Update(int id, Gasto gasto)
        {
            gasto.Id = id;
            var updated = await _service.UpdateAsync(gasto);
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