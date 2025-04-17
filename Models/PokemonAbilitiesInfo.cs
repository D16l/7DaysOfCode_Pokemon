namespace _7DaysOfCode_Pokemon.Models;
internal class PokemonAbilitiesInfo
{
    public Abilities Ability { get; set; }
    public AbilitiesDetail AbilitiesDetail { get; set; }
    public PokemonAbilitiesInfo (Abilities ability, AbilitiesDetail abilitiesDetail)
    {
        Ability = ability;
        AbilitiesDetail = abilitiesDetail;
    }
}