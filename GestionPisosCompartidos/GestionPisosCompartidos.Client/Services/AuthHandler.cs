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

            return await base.SendAsync(request, cancellationToken);
        }
    }
}