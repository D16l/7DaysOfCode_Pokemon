using System.Text.Json;
using System.Text.Json.Serialization;
using _7DaysOfCode_Pokemon.Models;

namespace _7DaysOfCode_Pokemon.Helpers;
public class PokemonPropertiesJsonList
{
    public static async Task<Pokemon> GetAsync(string url)
    {
        if (url == null) throw new ArgumentNullException(nameof(url), "A URL não pode ser nula.");
        using var client = new HttpClient();
        var endpoint = new Uri(url);

        var response = await client.GetStringAsync(endpoint);

        var options = new JsonSerializerOptions 
        { 
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        return JsonSerializer.Deserialize<Pokemon>(response, options)!;
    }
}