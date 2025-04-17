namespace _7DaysOfCode_Pokemon.FrontEnd.Menus;
internal class MenuAboutPokemon : MenuWrapper
{
    public MenuAboutPokemon() : base(" SOBRE ") { }
    public override void Start()
    {
        base.Start();
        Console.WriteLine(
            $"Nome Pokemon: {UserPokemonChoice?.Name}\n" +
            $"Altura: {UserPokemonChoice?.Height}\n" +
            $"Peso: {UserPokemonChoice?.Weight}\n" +
            $"Habilidades:"
        );
        foreach (var ability in UserPokemonChoice!.Abilities!)
        {
            Console.Write($"{ability?.Ability?.Name} ");
        }
        Console.ReadKey();
        UserMenuChoice = 2;
    }
}