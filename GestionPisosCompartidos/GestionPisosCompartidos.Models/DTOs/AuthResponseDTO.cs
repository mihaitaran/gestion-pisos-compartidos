namespace GestionPisosCompartidos.Models.DTOs
{
    public class AuthResponseDTO
    {
        public string Token { get; set; } = null!;
        public int UsuarioId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Rol { get; set; } = null!;
    }
}