using _7DaysOfCode_Pokemon.Helpers;
using System.Text.Json.Serialization;

namespace _7DaysOfCode_Pokemon.Models;
public class Pokemon
{
    [JsonInclude]public int? Id { get; private set; }
    private string? _name;
    [JsonInclude]public string? Name 
    { 
        get => _name;
        private set => _name = value?.ToUpper();
    }
    [JsonInclude]public string? Url { get; private set; }
    [JsonInclude]public int? Height { get; private set; }
    [JsonInclude]public int? Weight { get; private set; }
    [JsonInclude]public int? BaseExperience { get; private set; }
    [JsonInclude]public List<Abilities>? Abilities { get; private set; }
    [JsonConstructor] public Pokemon() { } //Construtor para o JsonSerializer

    private List<PokemonAbilitiesInfo> PokemonAbilities = new List<PokemonAbilitiesInfo>();

    private static List<Pokemon> Pokemons = new List<Pokemon>();

    public static List<string> List = new List<string>();

    private Pokemon(int? id, string name, string? url, int? height, int? weight, int? baseExperience, List<Abilities>? abilities)
    {
        Id = id;
        Name = name;
        Url = url;
        Height = height;
        Weight = weight;
        BaseExperience = baseExperience;
        Abilities = abilities;
        Pokemons.Add(this);
        List.Add(Name);
    }

    public static async Task CreatePokemonsAsync()
    {
        var pokemonList = await PokemonJsonList.GetAsync();
        if (pokemonList == null) 
            throw new ArgumentNullException(nameof(pokemonList), "Lista de pokemons não contém itens.");
        foreach (var pokemon in pokemonList.results!)
        {
            var pokemonProperties = await PokemonPropertiesJsonList.GetAsync(pokemon.Url!);
            var newPokemon = new Pokemon(pokemonProperties.Id,pokemon.Name!, pokemon.Url,pokemonProperties.Height,pokemonProperties.Weight,pokemonProperties.BaseExperience,pokemonProperties.Abilities);

            foreach (var prop in pokemonProperties.Abilities!)
            {
                if (!newPokemon.PokemonAbilities.Any(a => a.Ability?.Ability?.Name == prop.Ability?.Name))
                {
                    if (prop.Ability == null) continue;
                    newPokemon.PokemonAbilities.Add(new PokemonAbilitiesInfo(prop, prop.Ability!));
                }
            }
        }
    }

    public static Pokemon GetPokemon(string name)
    {
        if (string.IsNullOrEmpty(name))
        throw new ArgumentNullException("O nome precisa ter algum valor.");

        if (Pokemons.Exists(n => n.Name == name))
        {
            return Pokemons.Find(n => n.Name == name)!;
        }
        else
        {
            throw new KeyNotFoundException($"Pokemon com o nome '{name}' não encontrado");
        }
    }

    public static void RemovePokemon(Pokemon pokemon)
    {
        Pokemons.Remove(pokemon);
    }
}