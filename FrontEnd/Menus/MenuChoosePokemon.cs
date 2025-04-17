using _7DaysOfCode_Pokemon.Models;

namespace _7DaysOfCode_Pokemon.FrontEnd.Menus;
internal class MenuChoosePokemon : MenuWrapper
{
    public MenuChoosePokemon() : base(" ADOTAR UM MASCOTE ") { }
    public override void Start()
    {
        base.Start();
        Console.WriteLine($"{UserName} Escolha uma espécie:");
        foreach (var pokemon in Pokemon.List)
        {
            Console.WriteLine(pokemon);
        }
        var choice = Console.ReadLine()?.ToUpper();
        UserPokemonChoice =  Pokemon.GetPokemon(choice);
        UserMenuChoice = 2;
    }
}