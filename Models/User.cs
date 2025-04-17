namespace _7DaysOfCode_Pokemon.Models;
internal class User
{
    private static string? _userName;
    public static string? UserName 
    {   get => _userName;
        set => _userName = value?.ToUpper();
    }
    public static int UserMenuChoice { get; set; } = 0;
    public static Pokemon? UserPokemonChoice { get; set; }
    public static List<Pokemon> MyPokemons { get; set; } = new List<Pokemon>();
}
