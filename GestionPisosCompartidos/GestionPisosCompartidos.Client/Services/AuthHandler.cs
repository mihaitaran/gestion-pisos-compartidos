using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace GestionPisosCompartidos.Client.Services
{
    public class AuthHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;

        public AuthHandler(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var token = await _localStorage.GetItemAsStringAsync("jwt_token");
                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }
            catch
            {
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                try
                {
                    await _localStorage.RemoveItemAsync("jwt_token");
                    await _localStorage.RemoveItemAsync("user_data");
                }
                catch { }
            }

            return response;
        }
    }
}