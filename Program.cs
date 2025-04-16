using System.Text.Json;
using System.Net.Http;

namespace _7DaysOfCode_Pokemon;
public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Bem vindo(a)! Escolha um dos pokemons abaixos:");
        var pokemons = await PokemonJsonList.GetAsync();
        var names = pokemons.results.Select(p => p.Name).ToList();

        for (int i = 0; i < names.Count; i++) 
        {
            Console.WriteLine($"{i +1} - {names[i]}");
        }

        Pokemon.CreatePokemons(pokemons);

        var userChoice = Console.ReadLine();
        Console.Clear();
        if (Pokemon.ListOfAllPokemons.ContainsKey(userChoice))
        {
            var chosedPokemon = Pokemon.ListOfAllPokemons[userChoice];
            var pokemon = await PokemonPropertiesJsonList.GetAsync(chosedPokemon);
            Console.WriteLine($"Nome Pokemon: {pokemon.Name}");
            Console.WriteLine($"Altura: {pokemon.Height}");
            Console.WriteLine($"Peso: {pokemon.Weight}");
            Console.WriteLine("Habilidades:");
            foreach (var ability in pokemon.Abilities)
            {
                Console.WriteLine($"{ability.Ability.Name.ToUpper()}");
            }
        }
        else
        {
            Console.WriteLine("Pokemon não encontrado");
        }
    }
}