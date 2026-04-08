namespace GestionPisosCompartidos.Models.DTOs
{
    public class RegisterDTO
    {
        public string Nombre { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Telefono { get; set; }
        public string Rol { get; set; } = null!;
    }
}