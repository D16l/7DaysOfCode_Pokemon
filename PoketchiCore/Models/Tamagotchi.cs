using System.ComponentModel;
using System.Text.Json.Serialization;

namespace PoketchiCore.Models;
public partial class Tamagotchi
{
    public static DateTime TimeOfBirth { get; private set; } = DateTime.UtcNow;
    public static TimeSpan TimeLived { get; private set; }
    public static bool IsDead {  get; private set; }
    private static int _health;
    private static int _healCharges;
    private static int _happiness;
    private static int _hunger;
    private static int _cleanliness;
    private static int Clamp(int value,int max) => Math.Clamp(value, 0, max);
    public static int Id { get; private set; }
    public static string? Name { get; set; }
    public static int Height { get; private set; }
    public static int Weight { get; private set; }
    public static int Health 
    { 
        get => _health; 
        set 
        {
            _health = Clamp(value, 100);
            if (_health == 0) SetDeathState();
        }
    }
    private static int Happiness { get => _happiness; set => _happiness = Clamp(value,100); }
    private static int Hunger { get => _hunger; set => _hunger = Clamp(value,100); }
    private static int Cleanliness { get => _cleanliness; set => _cleanliness = Clamp(value,100); }

    private static int _experience;
    private static int Experience
    {
        get => _experience;
        set
        {
            int maxExperience = Evolutions.Count != 0
                ? Evolutions.First().Value.Item4
                : 1000;
            if (Evolutions.Count != 0 && value >= maxExperience) Evolve();
            _experience = Math.Clamp(value, _experience, maxExperience);
        }
    }

    
    private const int RegenMinutes = 180;
    private static DateTime _lastHealRegen = DateTime.UtcNow;
    public static int HealCharges
    {
        get
        {
            RegenerateHealCharges();
            return _healCharges;
        }
        set
        {
            _healCharges = Clamp(value, 3);
            HealChargesChanged?.Invoke();
        }
    }

    private const int MoodMinutes = 10;
    private static DateTime _lastPlayed = DateTime.UtcNow;
    private static MoodState _mood;
    public static MoodState Mood
    {
        get
        {
            SetMoodState();
            return _mood;
        }
    }

    private const int HungerMinutes = 30;
    private static DateTime _lastFed = DateTime.UtcNow;
    private static HungerState _appetite;
    public static HungerState Appetite
    {
        get
        {
            SetStomachState();
            return _appetite;
        }
    }

    private const int CleanlinessMinutes = 60;
    private static DateTime _lastBathed = DateTime.UtcNow;
    private static HygienicState _hygiene;
    public static HygienicState Hygiene
    {
        get
        {
            SetDirtyState();
            return _hygiene;
        }
    }
    private static Dictionary<string, Tuple<int, int, int, int>> Evolutions = [];

    public Tamagotchi()
    {
        if (Pokemon.This == null) return;

        var data = Pokemon.This.GetPokemonData();
        if (data.name == null) return;

        Id = data.id;
        Name = data.name;
        Health = data.health;
        Height = data.height;
        Weight = data.weight;
        Happiness = data.happiness;
        Hunger = data.hunger;
        Cleanliness = data.dirtness;
        Experience = data.experience;
        Evolutions = data.evolutions;
        IsDead = data.isDead;
        HealCharges = 3;
    }
}