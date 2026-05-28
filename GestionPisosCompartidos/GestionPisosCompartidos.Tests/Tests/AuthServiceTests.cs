using FluentAssertions;
using GestionPisosCompartidos.Models.DTOs;
using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Repositories.Interfaces;
using GestionPisosCompartidos.Services.Implementations;
using Microsoft.Extensions.Configuration;
using Moq;

namespace GestionPisosCompartidos.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<IUsuarioRepository> _repoMock;
        private readonly Mock<IConfiguration> _configMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _repoMock = new Mock<IUsuarioRepository>();
            _configMock = new Mock<IConfiguration>();

            _configMock.Setup(c => c["Jwt:Key"]).Returns("esta_es_una_clave_secreta_muy_larga_123456");
            _configMock.Setup(c => c["Jwt:Issuer"]).Returns("RoomMate");
            _configMock.Setup(c => c["Jwt:Audience"]).Returns("RoomMateUsers");
            _configMock.Setup(c => c["Jwt:ExpireMinutes"]).Returns("1440");

            _authService = new AuthService(_repoMock.Object, _configMock.Object);
        }

        [Fact]
        public async Task Login_ConEmailIncorrecto_DevuelveNull()
        {
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Usuario>());

            var loginDto = new LoginDTO { Email = "noexiste@gmail.com", Password = "Password1!" };

            var resultado = await _authService.LoginAsync(loginDto);

            resultado.Should().BeNull();
        }

        [Fact]
        public async Task Login_ConPasswordIncorrecta_DevuelveNull()
        {
            var usuario = new Usuario
            {
                Id = 1,
                Email = "test@gmail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password1!"),
                Nombre = "Test",
                Apellidos = "Usuario",
                Rol = "Inquilino",
                Activo = true
            };

            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Usuario> { usuario });

            var loginDto = new LoginDTO { Email = "test@gmail.com", Password = "PasswordIncorrecta!" };

            var resultado = await _authService.LoginAsync(loginDto);

            resultado.Should().BeNull();
        }

        [Fact]
        public async Task Login_ConCredencialesCorrectas_DevuelveToken()
        {
            var usuario = new Usuario
            {
                Id = 1,
                Email = "test@gmail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password1!"),
                Nombre = "Test",
                Apellidos = "Usuario",
                Rol = "Propietario",
                Activo = true
            };

            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Usuario> { usuario });

            var loginDto = new LoginDTO { Email = "test@gmail.com", Password = "Password1!" };

            var resultado = await _authService.LoginAsync(loginDto);

            resultado.Should().NotBeNull();
            resultado!.Token.Should().NotBeNullOrEmpty();
            resultado.Rol.Should().Be("Propietario");
        }

        [Fact]
        public async Task Register_ConEmailDuplicado_DevuelveNull()
        {
            var usuario = new Usuario
            {
                Id = 1,
                Email = "test@gmail.com",
                PasswordHash = "hash",
                Nombre = "Test",
                Apellidos = "Usuario",
                Rol = "Inquilino",
                Activo = true
            };

            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Usuario> { usuario });

            var registerDto = new RegisterDTO
            {
                Email = "test@gmail.com",
                Password = "Password1!",
                Nombre = "Otro",
                Apellidos = "Usuario",
                Rol = "Inquilino"
            };

            var resultado = await _authService.RegisterAsync(registerDto);

            resultado.Should().BeNull();
        }
    }
}