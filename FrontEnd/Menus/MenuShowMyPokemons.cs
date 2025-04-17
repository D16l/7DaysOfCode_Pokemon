namespace _7DaysOfCode_Pokemon.FrontEnd.Menus;
internal class MenuShowMyPokemons : MenuWrapper
{
    public MenuShowMyPokemons() : base(" MEUS MASCOTES ") { }

    public override void Start()
    {
        base.Start();
        Console.WriteLine("Meus mascotes:");
        foreach (var pokemon in MyPokemons)
        {
            Console.WriteLine(pokemon.Name);
        }
        Console.ReadKey();
        UserMenuChoice = 1;
    }
}
