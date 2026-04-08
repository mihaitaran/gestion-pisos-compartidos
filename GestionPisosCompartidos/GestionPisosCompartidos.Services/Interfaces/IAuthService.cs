using GestionPisosCompartidos.Models.DTOs;

namespace GestionPisosCompartidos.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDTO?> LoginAsync(LoginDTO loginDto);
        Task<AuthResponseDTO?> RegisterAsync(RegisterDTO registerDto);
    }
}