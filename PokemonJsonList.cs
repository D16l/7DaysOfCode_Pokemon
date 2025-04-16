using System.Text.Json;

namespace _7DaysOfCode_Pokemon;
public class PokemonJsonList
{
    public List<Pokemon> results { get; set; }

    public static async Task<PokemonJsonList> GetAsync()
    {
        using var client = new HttpClient();
        var endpoint = "https://pokeapi.co/api/v2/pokemon/?limit=100";

        var response = await client.GetStringAsync(endpoint);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var list = JsonSerializer.Deserialize<PokemonJsonList>(response, options);

        return list ?? new PokemonJsonList { results = new List<Pokemon>() };
    }
}
