using Microsoft.JSInterop;
using System.Net.Http.Headers;

namespace GestionPisosCompartidos.Client.Services
{
    public class AuthHandler : DelegatingHandler
    {
        private readonly IJSRuntime _js;

        public AuthHandler(IJSRuntime js)
        {
            _js = js;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var token = await _js.InvokeAsync<string?>("authStorage.getToken");
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