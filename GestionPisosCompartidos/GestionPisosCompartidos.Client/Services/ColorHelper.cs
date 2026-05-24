using MudBlazor;

namespace GestionPisosCompartidos.Client.Services
{
    public static class ColorHelper
    {
        public static Color GetEstadoColor(string estado)
        {
            return estado switch
            {
                "Pagado" => Color.Success,
                "Pendiente" => Color.Warning,
                "Rechazado" => Color.Error,
                _ => Color.Default
            };
        }

        public static Color GetPrioridadColor(string prioridad)
        {
            return prioridad switch
            {
                "Baja" => Color.Info,
                "Media" => Color.Warning,
                "Alta" => Color.Error,
                "Urgente" => Color.Error,
                _ => Color.Default
            };
        }

        public static Color GetEstadoIncidenciaColor(string estado)
        {
            return estado switch
            {
                "Abierta" => Color.Error,
                "EnProceso" => Color.Warning,
                "Resuelta" => Color.Success,
                "Cerrada" => Color.Default,
                _ => Color.Default
            };
        }
    }
}