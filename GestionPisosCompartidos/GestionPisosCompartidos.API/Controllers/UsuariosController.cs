using GestionPisosCompartidos.Models.DTOs;
using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionPisosCompartidos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuariosController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetAll()
        {
            var usuarios = await _service.GetAllAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            var usuario = await _service.GetByIdAsync(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Create(Usuario usuario)
        {
            var created = await _service.CreateAsync(usuario);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> Update(int id, Usuario usuario)
        {
            usuario.Id = id;
            var updated = await _service.UpdateAsync(usuario);
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

        [HttpPatch("{id}/password")]
        public async Task<ActionResult> CambiarPassword(int id, [FromBody] CambiarPasswordDTO dto)
        {
            try
            {
                var resultado = await _service.CambiarPasswordAsync(id, dto.PasswordActual, dto.PasswordNueva);
                if (!resultado) return BadRequest(new { message = "Contraseña actual incorrecta" });
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPatch("{id}/perfil")]
        public async Task<ActionResult> ActualizarPerfil(int id, [FromBody] ActualizarPerfilDTO dto)
        {
            var usuario = await _service.GetByIdAsync(id);
            if (usuario == null) return NotFound();

            usuario.Nombre = dto.Nombre;
            usuario.Apellidos = dto.Apellidos ?? usuario.Apellidos;
            usuario.Telefono = dto.Telefono ?? usuario.Telefono;

            await _service.UpdateAsync(usuario);
            return Ok();
        }
    }
}