using Blazored.LocalStorage;
using System.Net.Http.Json;

namespace GestionPisosCompartidos.Client.Services
{
    public class AuthService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;

        public AuthService(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            await LogoutAsync();

            var loginData = new { Email = email, Password = password };
            var response = await _http.PostAsJsonAsync("api/auth/login", loginData);

            if (!response.IsSuccessStatusCode) return false;

            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
            if (result == null) return false;

            await _localStorage.SetItemAsStringAsync("jwt_token", result.Token);
            await _localStorage.SetItemAsync("user_data", new UserData
            {
                UsuarioId = result.UsuarioId,
                Nombre = result.Nombre,
                Email = result.Email,
                Rol = result.Rol
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
            return await _localStorage.GetItemAsStringAsync("jwt_token");
        }

        public async Task<UserData?> GetUserAsync()
        {
            return await _localStorage.GetItemAsync<UserData>("user_data");
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("jwt_token");
            await _localStorage.RemoveItemAsync("user_data");
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