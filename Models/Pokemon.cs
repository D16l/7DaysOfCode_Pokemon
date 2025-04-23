using _7DaysOfCode_Pokemon.Controller;
using System.Text.Json;
namespace _7DaysOfCode_Pokemon.Models;

public class Pokemon
{
    public static Pokemon? This { get; private set; }
    public static int Id { get; private set; }
    private static int CurrentChainId { get; set; }

    private string Name { get; set; }
    private int Health { get; set; }
    private int Height { get; set; }
    private int Weight { get; set; }
    private int Happiness { get; set; }
    private int Hunger { get; set; }
    private HygienicState Hygiene { get; set; }
    private int Experience { get; set; }


    private static Dictionary<string, Tuple<int,int,int,int>> Evolutions = []; //Dictionary<pokemon,<id,height,weight,experience>>
    private Pokemon(int id, string name, int health,int height, int weight, int happiness, int hunger, HygienicState hygiene, int experience)
    {
        Id = id;
        Name = name.ToUpper();
        Health = health;
        Height = height;
        Weight = weight;
        Happiness = happiness;
        Hunger = hunger;
        Hygiene = hygiene;
        Experience = experience;
        This = this;
    }
    public static async Task CreatePokemonAsync()
    {
        int randomIDPokemon;
        do { randomIDPokemon = new Random().Next(0, 550);} 
        while (randomIDPokemon == CurrentChainId);

        CurrentChainId = randomIDPokemon;

        var jsonEvolutionChain = await PokemonAPIJson.GetAsync("https://pokeapi.co/api/v2/evolution-chain/", randomIDPokemon);

        JsonElement chain = jsonEvolutionChain.GetProperty("chain");
        await ExtractDataFromJson(chain);
        new Tamagotchi();
    }

    private static async Task ExtractDataFromJson(JsonElement currentChain, bool isRoot = true)
    {
        string name = currentChain.GetProperty("species").GetProperty("name").GetString()!;
        int id = int.Parse(currentChain.GetProperty("species").GetProperty("url").GetString()!.Split('/').Reverse().Skip(1).First());

        var jsonHappiness = await PokemonAPIJson.GetAsync("https://pokeapi.co/api/v2/pokemon-species/", id);
        int baseHappiness = jsonHappiness.GetProperty("base_happiness").GetInt16();

        var jsonPokemon = await PokemonAPIJson.GetAsync("https://pokeapi.co/api/v2/pokemon/", id);
        int baseExperience = jsonPokemon.GetProperty("base_experience").GetInt16();
        int height = jsonPokemon.GetProperty("height").GetInt16();
        int weight = jsonPokemon.GetProperty("weight").GetInt16();

        if (isRoot)
        {
            new Pokemon(id, name, 100, height, weight, 50, 50, hygiene: HygienicState.Dirty, baseExperience);
        }
        else
        {
            Evolutions.Add(name, new Tuple<int, int, int, int>(id, height, weight, baseExperience));
        }

        if (currentChain.TryGetProperty("evolves_to", out JsonElement evolvesTo) && evolvesTo.ValueKind == JsonValueKind.Array)
        {
            foreach (var evolve in evolvesTo.EnumerateArray())
            {
                await ExtractDataFromJson(evolve, isRoot = false);
            }
        }
    }

    public (int id, string name, int health, int height, int weight, int happiness, int hunger, HygienicState hygiene, int experience, Dictionary<string, Tuple<int, int, int, int>> evolutions) GetPokemonData()
    {
        return (Id, Name, Health, Height, Weight, Happiness, Hunger, Hygiene, Experience, Evolutions);
    }
}