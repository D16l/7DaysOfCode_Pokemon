namespace _7DaysOfCode_Pokemon.Models;
public class AbilitiesDetail
{
    private string? _name;
    public string? Name 
    {
        get => _name;
        set => _name = value?.ToUpper();
    }
    public string? Url { get; set; }
}