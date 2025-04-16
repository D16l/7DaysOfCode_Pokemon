using System.Text.Json;

namespace _7DaysOfCode_Pokemon;
public class Pokemon
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public int BaseExperience { get; set; }
    public List<Abilities> Abilities { get; set; }

    public static Dictionary<string, string?> ListOfAllPokemons = [];

    public Pokemon(string? name, string? url)
    {
        Name = name;
        Url = url;
        ListOfAllPokemons.TryAdd(name, url);
    }

    public static void CreatePokemons(PokemonJsonList pokemonList)
    {
        foreach (var item in pokemonList.results)
        {
            var pokemon = new Pokemon(item.Name, item.Url);
        }
    }
}
