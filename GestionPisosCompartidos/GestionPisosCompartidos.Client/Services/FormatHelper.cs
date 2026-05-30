namespace GestionPisosCompartidos.Client.Services
{
    public static class FormatHelper
    {
        public static string FormatFecha(string? fecha)
        {
            if (string.IsNullOrEmpty(fecha)) return "-";
            if (DateTime.TryParse(fecha, out var f))
                return f.ToString("dd/MM/yyyy");
            return fecha;
        }

        public static string FormatFechaHora(string? fecha)
        {
            if (string.IsNullOrEmpty(fecha)) return "-";
            if (DateTime.TryParse(fecha, out var f))
                return f.ToString("dd/MM/yyyy HH:mm");
            return fecha;
        }
    }
}