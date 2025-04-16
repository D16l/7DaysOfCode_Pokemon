using System.Text.Json;

namespace _7DaysOfCode_Pokemon;
public class PokemonPropertiesJsonList
{
    public static async Task<Pokemon> GetAsync(string url)
    {
        using var client = new HttpClient();
        var endpoint = new Uri(url);

        var response = await client.GetStringAsync(endpoint);

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        return JsonSerializer.Deserialize<Pokemon>(response,options);
    }
}