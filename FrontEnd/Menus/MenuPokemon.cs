using _7DaysOfCode_Pokemon.Models;

namespace _7DaysOfCode_Pokemon.FrontEnd.Menus;
internal class MenuPokemon : MenuWrapper
{
    public MenuPokemon() : base($" {UserPokemonChoice?.Name} ") { }
    public override void Start()
    {
        base.Start();
        Console.WriteLine($"1 - Saber mais sobre {UserPokemonChoice?.Name}");
        Console.WriteLine($"2 - Adotar {UserPokemonChoice?.Name}");
        Console.WriteLine("3 - Voltar");

        switch (Console.ReadLine())
        {
            case "1":
                UserMenuChoice = 5;
                break;
            case "2":
                MyPokemons?.Add(UserPokemonChoice!);
                Pokemon.List.Remove(UserPokemonChoice!.Name!);
                Pokemon.RemovePokemon(UserPokemonChoice);
                UserMenuChoice = 6;
                break;
            case "3":
                UserMenuChoice = 1;
                break;
            default:
                Console.Clear();
                Console.WriteLine("Opção inválida");
                UserMenuChoice = 2;
                break;
        }
    }
}
