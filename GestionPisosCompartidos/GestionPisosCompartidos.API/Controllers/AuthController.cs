using GestionPisosCompartidos.Models.DTOs;
using GestionPisosCompartidos.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestionPisosCompartidos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login(LoginDTO loginDto)
        {
            var response = await _authService.LoginAsync(loginDto);
            if (response == null)
                return Unauthorized(new { message = "Email o contraseña incorrectos" });

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDTO>> Register(RegisterDTO registerDto)
        {
            var response = await _authService.RegisterAsync(registerDto);
            if (response == null)
                return BadRequest(new { message = "El email ya está registrado" });

            return Ok(response);
        }
    }
}