using GestionPisosCompartidos.API.Hubs;
using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GestionPisosCompartidos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MensajesController : ControllerBase
    {
        private readonly IMensajeService _service;
        private readonly IHubContext<ChatHub> _hubContext;

        public MensajesController(IMensajeService service, IHubContext<ChatHub> hubContext)
        {
            _service = service;
            _hubContext = hubContext;
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

            var mensajes = await _service.GetByViviendaIdAsync(created.ViviendaId);
            var mensajeCompleto = mensajes.LastOrDefault();

            if (mensajeCompleto != null)
            {
                await _hubContext.Clients.Group($"vivienda_{created.ViviendaId}")
                    .SendAsync("RecibirMensaje", new
                    {
                        mensajeCompleto.Id,
                        mensajeCompleto.ViviendaId,
                        mensajeCompleto.EmisorId,
                        mensajeCompleto.Contenido,
                        FechaEnvio = mensajeCompleto.FechaEnvio.ToString(),
                        Emisor = mensajeCompleto.Emisor != null ? new
                        {
                            mensajeCompleto.Emisor.Id,
                            mensajeCompleto.Emisor.Nombre,
                            mensajeCompleto.Emisor.Apellidos
                        } : null
                    });
            }

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