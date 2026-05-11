using Microsoft.AspNetCore.SignalR;

namespace GestionPisosCompartidos.API.Hubs
{
    public class ChatHub : Hub
    {
        public async Task UnirseAVivienda(int viviendaId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"vivienda_{viviendaId}");
        }

        public async Task SalirDeVivienda(int viviendaId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"vivienda_{viviendaId}");
        }
    }
}