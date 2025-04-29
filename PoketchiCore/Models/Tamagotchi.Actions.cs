namespace PoketchiCore.Models;
public partial class Tamagotchi
{
    public static event Action? OnStatusUpdated;
    public static event Action? HealChargesChanged;
    public static event Action? OnEvolve;
    public static event Action? OnDeath;
    public static void Heal()
    {
        RegenerateHealCharges();
        if (Health == 100) return;
        if (_healCharges > 0)
        {
            _healCharges--;

            if (Health < 20) Happiness += 10;
            else if (Health < 80) Happiness += 5;

            Health += 20;
        }
        OnStatusUpdated?.Invoke();
    }

    public static void Play()
    {
        var now = DateTime.UtcNow;
        var timeBetweenLastPlay = now - _lastPlayed;
        if (timeBetweenLastPlay.TotalSeconds < 10) return;
        _lastPlayed = now;
        Happiness += 25;
        Hunger -= 3;
        OnStatusUpdated?.Invoke();
    }

    public static void Feed()
    {
        if (Hunger == 100)
            return;
        _lastFed = DateTime.UtcNow;
        if (Hunger < 20) Cleanliness -= 20;
        Hunger += 20;
        Happiness += 10;
        OnStatusUpdated?.Invoke();
    }

    public static void Bath()
    {
        if (Cleanliness == 100)
            return;
        _lastBathed = DateTime.UtcNow;
        Cleanliness += 50;
        Happiness += 10;
        OnStatusUpdated?.Invoke();
    }

    public static void Train()
    {
        if (Happiness < 75) Experience += 1;
        else if (Hunger < 60) Experience += 1;
        else if (Cleanliness < 20) Experience += 1;
        else Experience += 3;

        Cleanliness -= 15;
        Hunger -= 5;
        OnStatusUpdated?.Invoke();
    }
}