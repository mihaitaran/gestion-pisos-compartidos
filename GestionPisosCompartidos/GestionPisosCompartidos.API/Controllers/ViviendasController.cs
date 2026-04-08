using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionPisosCompartidos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ViviendasController : ControllerBase
    {
        private readonly IViviendaService _service;

        public ViviendasController(IViviendaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Vivienda>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vivienda>> GetById(int id)
        {
            var vivienda = await _service.GetByIdAsync(id);
            if (vivienda == null) return NotFound();
            return Ok(vivienda);
        }

        [HttpGet("propietario/{propietarioId}")]
        public async Task<ActionResult<List<Vivienda>>> GetByPropietario(int propietarioId)
        {
            return Ok(await _service.GetByPropietarioIdAsync(propietarioId));
        }

        [HttpPost]
        public async Task<ActionResult<Vivienda>> Create(Vivienda vivienda)
        {
            var created = await _service.CreateAsync(vivienda);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Vivienda>> Update(int id, Vivienda vivienda)
        {
            vivienda.Id = id;
            var updated = await _service.UpdateAsync(vivienda);
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