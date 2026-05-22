using GestionPisosCompartidos.Services.Interfaces;
using System.Text.Json;

namespace GestionPisosCompartidos.Services.Integrations
{
    public class LugaresService : ILugaresService
    {
        private readonly HttpClient _httpClient;

        public LugaresService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "GestionPisosCompartidos/1.0");
        }

        public async Task<List<LugarCercanoDTO>> BuscarLugaresCercanosAsync(double latitud, double longitud, string tipo, int radio = 1500)
        {
            var overpassTipo = ConvertirTipo(tipo);

            var query = $@"[out:json][timeout:25];
                (
                    node[{overpassTipo}](around:{radio},{latitud.ToString(System.Globalization.CultureInfo.InvariantCulture)},{longitud.ToString(System.Globalization.CultureInfo.InvariantCulture)});
                    way[{overpassTipo}](around:{radio},{latitud.ToString(System.Globalization.CultureInfo.InvariantCulture)},{longitud.ToString(System.Globalization.CultureInfo.InvariantCulture)});
                );
                out center;";

            var url = $"https://overpass-api.de/api/interpreter?data={Uri.EscapeDataString(query)}";

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(content);

            var lugares = new List<LugarCercanoDTO>();
            var elements = json.RootElement.GetProperty("elements");

            foreach (var element in elements.EnumerateArray())
            {
                double lat = 0, lon = 0;

                if (element.TryGetProperty("lat", out var latProp))
                    lat = latProp.GetDouble();
                else if (element.TryGetProperty("center", out var center))
                {
                    lat = center.GetProperty("lat").GetDouble();
                    lon = center.GetProperty("lon").GetDouble();
                }

                if (element.TryGetProperty("lon", out var lonProp))
                    lon = lonProp.GetDouble();

                var tags = element.TryGetProperty("tags", out var tagsElement) ? tagsElement : default;

                var nombre = "";
                if (tags.ValueKind != JsonValueKind.Undefined)
                {
                    if (tags.TryGetProperty("name", out var nameProp))
                        nombre = nameProp.GetString() ?? "";
                }

                if (string.IsNullOrEmpty(nombre)) continue;

                var direccion = "";
                if (tags.ValueKind != JsonValueKind.Undefined)
                {
                    var calle = tags.TryGetProperty("addr:street", out var streetProp) ? streetProp.GetString() ?? "" : "";
                    var numero = tags.TryGetProperty("addr:housenumber", out var numProp) ? numProp.GetString() ?? "" : "";
                    direccion = !string.IsNullOrEmpty(calle) ? $"{calle} {numero}".Trim() : "";
                }

                lugares.Add(new LugarCercanoDTO
                {
                    Nombre = nombre,
                    Direccion = direccion,
                    Latitud = lat,
                    Longitud = lon,
                    Tipo = tipo
                });
            }

            return lugares;
        }

        public async Task<(double lat, double lng)?> GeocodificarDireccionAsync(string direccion)
        {
            var url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(direccion)}&format=json&limit=1";

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(content);

            var results = json.RootElement;
            if (results.GetArrayLength() > 0)
            {
                var lat = double.Parse(results[0].GetProperty("lat").GetString()!, System.Globalization.CultureInfo.InvariantCulture);
                var lng = double.Parse(results[0].GetProperty("lon").GetString()!, System.Globalization.CultureInfo.InvariantCulture);
                return (lat, lng);
            }

            return null;
        }

        private string ConvertirTipo(string tipo)
        {
            return tipo switch
            {
                "supermarket" => "\"shop\"=\"supermarket\"",
                "pharmacy" => "\"amenity\"=\"pharmacy\"",
                "hospital" => "\"amenity\"=\"hospital\"",
                "restaurant" => "\"amenity\"=\"restaurant\"",
                "gym" => "\"leisure\"=\"fitness_centre\"",
                "park" => "\"leisure\"=\"park\"",
                "school" => "\"amenity\"=\"school\"",
                "cafe" => "\"amenity\"=\"cafe\"",
                "bank" => "\"amenity\"=\"bank\"",
                _ => "\"shop\"=\"supermarket\""
            };
        }
    }
}