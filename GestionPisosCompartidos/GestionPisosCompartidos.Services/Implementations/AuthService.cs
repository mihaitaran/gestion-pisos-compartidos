using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GestionPisosCompartidos.Models.DTOs;
using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Repositories.Interfaces;
using GestionPisosCompartidos.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GestionPisosCompartidos.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponseDTO?> LoginAsync(LoginDTO loginDto)
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            var usuario = usuarios.FirstOrDefault(u => u.Email == loginDto.Email);

            if (usuario == null) return null;

            // Verificar contraseña
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, usuario.PasswordHash))
                return null;

            return GenerateResponse(usuario);
        }

        public async Task<AuthResponseDTO?> RegisterAsync(RegisterDTO registerDto)
        {
            // Comprobar si el email ya existe
            var usuarios = await _usuarioRepository.GetAllAsync();
            if (usuarios.Any(u => u.Email == registerDto.Email))
                return null;

            var usuario = new Usuario
            {
                Nombre = registerDto.Nombre,
                Apellidos = registerDto.Apellidos,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Telefono = registerDto.Telefono,
                Rol = registerDto.Rol,
                FechaRegistro = DateTime.UtcNow,
                Activo = true
            };

            var created = await _usuarioRepository.CreateAsync(usuario);
            return GenerateResponse(created);
        }

        private AuthResponseDTO GenerateResponse(Usuario usuario)
        {
            var token = GenerateToken(usuario);
            return new AuthResponseDTO
            {
                Token = token,
                UsuarioId = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Rol = usuario.Rol
            };
        }

        private string GenerateToken(Usuario usuario)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    double.Parse(_configuration["Jwt:ExpireMinutes"]!)),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}