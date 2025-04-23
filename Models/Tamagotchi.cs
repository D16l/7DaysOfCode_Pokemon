namespace _7DaysOfCode_Pokemon.Models;
public class Tamagotchi
{
    public static Tamagotchi? This { get; set; }
    public static int Id { get; private set; }
    private string? Name { get; set; }
    private int Height { get; set; }
    private int Weight { get; set; }



    private int _health;
    public int Health 
    { 
        get => _health; 
        set => _health = Math.Clamp(value,0,MaxHealth); 
    }
    private const int MaxHealth = 100;
    private const int MaxHealCharges = 3;
    private const int RegenMinutes = 180;
    private int _healCharges = MaxHealCharges;
    private DateTime _lastHealRegen = DateTime.UtcNow;
    public int HealCharges
    {
        get
        {
            RegenerateHealCharges();
            return _healCharges;
        }
        set => _healCharges = Math.Clamp(value,0,MaxHealCharges);
    }


    private int _happiness;
    private int Happiness 
    {
        get => _happiness; 
        set => _happiness = Math.Clamp(value,0,100); 
    }
    private const int MoodMinutes = 10;
    private DateTime _lastPlayed = DateTime.UtcNow;
    private MoodState _mood;
    public MoodState Mood
    {
        get
        {
            SetMoodState();
            return _mood;
        }
    }



    private int _hunger;
    private int Hunger 
    { 
        get => _hunger; 
        set => _hunger = Math.Clamp(value,0,100); 
    }
    private int HungerMinutes = 30;
    private DateTime _lastFed = DateTime.UtcNow;
    private StomachState _stomachState;
    public StomachState StomachState
    {
        get
        {
            return _stomachState;
        }
    }






    private HygienicState Hygiene { get; set; }
    private int Experience { get; set; }

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
        Hygiene = data.hygiene;
        Experience = data.experience;
        Evolutions = data.evolutions;
        This = this;
    }

    public void Heal()
    {
        RegenerateHealCharges();
        if (Health >= MaxHealth) return;
        if (_healCharges > 0)
        {
            _healCharges--;
            Health = Math.Min(MaxHealth, Health + 20);
        }
    }

    private void RegenerateHealCharges()
    {
        var now = DateTime.UtcNow;
        var timeBetweenLastCharge = now - _lastHealRegen;
        int regenCount = (int)(timeBetweenLastCharge.TotalMinutes / RegenMinutes);

        if (regenCount > 0 && _healCharges < MaxHealCharges)
        {
            _healCharges = Math.Min(MaxHealCharges, _healCharges + regenCount);
            _lastHealRegen = _lastHealRegen.AddMinutes(regenCount * RegenMinutes);
        }
    }

    public void Play()
    {
        var now = DateTime.UtcNow;
        var timeBetweenLastPlay = now - _lastPlayed;
        if (timeBetweenLastPlay.TotalSeconds < 10) return;
        _lastPlayed = now;
        Happiness += 25;
    }

    public void SetMoodState()
    {
        var now = DateTime.UtcNow;
        var timeBetweenLastPlay = now - _lastPlayed;
        if (timeBetweenLastPlay.TotalMinutes >= MoodMinutes) Happiness -= 1;
        if (Health < 20) Happiness -= 3;



        if (Happiness >= (int)MoodState.Ecstatic)      _mood = MoodState.Ecstatic;
        else if (Happiness >= (int)MoodState.Happy)    _mood = MoodState.Happy;
        else if (Happiness >= (int)MoodState.Content)  _mood = MoodState.Content;
        else if (Happiness >= (int)MoodState.Bored)    _mood = MoodState.Bored;
        else if (Happiness >= (int)MoodState.Unhappy)  _mood = MoodState.Unhappy;
        else if (Happiness >= (int)MoodState.Sad)      _mood = MoodState.Sad;
        else                                           _mood = MoodState.Miserable;
    }

    public void Feed()
    {
        if (Hunger == 100)
            return;
        _lastFed = DateTime.UtcNow;
        Hunger += 20;
    }

    public void SetStomachState()
    {
        var now = DateTime.UtcNow;
        var timeBetweenLastMeal = now - _lastFed;
        if (timeBetweenLastMeal.TotalMinutes >= HungerMinutes) Hunger -= 5;

        if (Hunger >= (int)StomachState.Full)           _stomachState = StomachState.Full;
        else if (Hunger >= (int)StomachState.Satisfied) _stomachState = StomachState.Satisfied;
        else if (Hunger >= (int)StomachState.Peckish)   _stomachState = StomachState.Peckish;
        else if (Hunger >= (int)StomachState.Hungry)    _stomachState = StomachState.Hungry;
        else if (Hunger >= (int)StomachState.Starving)  _stomachState = StomachState.Starving;
        else                                            _stomachState = StomachState.Famished;
    }
}
