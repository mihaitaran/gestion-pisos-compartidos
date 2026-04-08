using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionPisosCompartidos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InquilinosViviendasController : ControllerBase
    {
        private readonly IInquilinoViviendaService _service;

        public InquilinosViviendasController(IInquilinoViviendaService service)
        {
            _service = service;
        }

        [HttpGet("vivienda/{viviendaId}")]
        public async Task<ActionResult<List<InquilinosVivienda>>> GetByVivienda(int viviendaId)
        {
            return Ok(await _service.GetByViviendaIdAsync(viviendaId));
        }

        [HttpGet("inquilino/{inquilinoId}")]
        public async Task<ActionResult<List<InquilinosVivienda>>> GetByInquilino(int inquilinoId)
        {
            return Ok(await _service.GetByInquilinoIdAsync(inquilinoId));
        }

        [HttpGet("vivienda/{viviendaId}/activos")]
        public async Task<ActionResult<List<InquilinosVivienda>>> GetActivosByVivienda(int viviendaId)
        {
            return Ok(await _service.GetActivosByViviendaIdAsync(viviendaId));
        }

        [HttpPost]
        public async Task<ActionResult<InquilinosVivienda>> Create(InquilinosVivienda inquilinoVivienda)
        {
            var created = await _service.CreateAsync(inquilinoVivienda);
            return Ok(created);
        }

        [HttpPatch("{id}/desactivar")]
        public async Task<ActionResult<InquilinosVivienda>> Desactivar(int id)
        {
            var updated = await _service.DesactivarAsync(id);
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