using System.Text.Json;
using System.Text.Json.Serialization;
using _7DaysOfCode_Pokemon.Models;

namespace _7DaysOfCode_Pokemon.Helpers;
public class PokemonJsonList
{
    public List<Pokemon>? results { get; set; }

    public static async Task<PokemonJsonList> GetAsync()
    {
        using var client = new HttpClient();
        var endpoint = "https://pokeapi.co/api/v2/pokemon/?limit=101";

        var response = await client.GetStringAsync(endpoint);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var list = JsonSerializer.Deserialize<PokemonJsonList>(response, options);

        return list ?? new PokemonJsonList { results = new List<Pokemon>() };
    }
}
