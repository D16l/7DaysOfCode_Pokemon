using _7DaysOfCode_Pokemon.Controller;
using _7DaysOfCode_Pokemon.Models;
using System.Drawing;

namespace _7DaysOfCode_Pokemon;
public class Program
{
    public static async Task Main(string[] args)
    {
        //Console.Write("Carregando");
        //var task = Pokemon.CreatePokemonsAsync();
        //while (!task.IsCompleted)
        //{
        //    Console.Write(".");
        //    await Task.Delay(1000);
        //}
        //await task;

        //Dictionary<int, Action> menus = new()
        //{
        //    {0, () => new MenuUser().Start() },
        //    {1, () => new MenuMain().Start() },
        //    {2, () => new MenuPokemon().Start() },
        //    {3, () => new MenuShowMyPokemons().Start() },
        //    {4, () => new MenuChoosePokemon().Start() },
        //    {5, () => new MenuAboutPokemon().Start() },
        //    {6, () => new MenuAdoptPokemon().Start() }
        //};

        //while (true)
        //{
        //    if (menus.ContainsKey(User.UserMenuChoice))
        //    {
        //        menus[User.UserMenuChoice].Invoke();
        //    }
        //}
        await Pokemon.CreatePokemonAsync();
        await SpritesDownloader.DownloadAsync();

        Console.WriteLine(Pokemon.Id);
        
    }
}