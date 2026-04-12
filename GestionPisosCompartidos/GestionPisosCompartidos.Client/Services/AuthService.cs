using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace GestionPisosCompartidos.Client.Services
{
    public class AuthService
    {
        private readonly IJSRuntime _js;
        private readonly HttpClient _http;

        public AuthService(IJSRuntime js, HttpClient http)
        {
            _js = js;
            _http = http;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var loginData = new { Email = email, Password = password };
            var response = await _http.PostAsJsonAsync("api/auth/login", loginData);

            if (!response.IsSuccessStatusCode) return false;

            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
            if (result == null) return false;

            await _js.InvokeVoidAsync("authStorage.setToken", result.Token);
            await _js.InvokeVoidAsync("authStorage.setUser", new
            {
                result.UsuarioId,
                result.Nombre,
                result.Email,
                result.Rol
            });

            return true;
        }

        public async Task<bool> RegisterAsync(string nombre, string apellidos, string email, string password, string telefono, string rol)
        {
            var registerData = new
            {
                Nombre = nombre,
                Apellidos = apellidos,
                Email = email,
                Password = password,
                Telefono = telefono,
                Rol = rol
            };

            var response = await _http.PostAsJsonAsync("api/auth/register", registerData);
            return response.IsSuccessStatusCode;
        }

        public async Task<string?> GetTokenAsync()
        {
            return await _js.InvokeAsync<string?>("authStorage.getToken");
        }

        public async Task<UserData?> GetUserAsync()
        {
            return await _js.InvokeAsync<UserData?>("authStorage.getUser");
        }

        public async Task LogoutAsync()
        {
            await _js.InvokeVoidAsync("authStorage.logout");
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var token = await GetTokenAsync();
            return !string.IsNullOrEmpty(token);
        }
    }

    public class AuthResponse
    {
        public string Token { get; set; } = "";
        public int UsuarioId { get; set; }
        public string Nombre { get; set; } = "";
        public string Email { get; set; } = "";
        public string Rol { get; set; } = "";
    }

    public class UserData
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; } = "";
        public string Email { get; set; } = "";
        public string Rol { get; set; } = "";
    }
}