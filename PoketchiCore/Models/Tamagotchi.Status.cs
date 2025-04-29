using PoketchiCore.Controller;

namespace PoketchiCore.Models;
public partial class Tamagotchi
{
    
    private static void RegenerateHealCharges()
    {
        var now = DateTime.UtcNow;
        var timeBetweenLastCharge = now - _lastHealRegen;
        int regenCount = (int)(timeBetweenLastCharge.TotalMinutes / RegenMinutes);

        if (regenCount > 0 && _healCharges < 3)
        {
            _healCharges = Math.Min(3, _healCharges + regenCount);
            _lastHealRegen = _lastHealRegen.AddMinutes(regenCount * RegenMinutes);
        }
    }
    private static void SetMoodState()
    {
        var now = DateTime.UtcNow;
        var timeBetweenLastPlay = now - _lastPlayed;
        if (timeBetweenLastPlay.TotalMinutes >= MoodMinutes) Happiness -= 1;
        if (Health < 20) Happiness -= 3;
        if (Happiness < 20) Hunger -= 1;
        if (Happiness == 0) 
        { 
            Health -= 5;
            Hunger -= 5;
        }

        if (Happiness >= (int)MoodState.Ecstatic) _mood = MoodState.Ecstatic;
        else if (Happiness >= (int)MoodState.Happy) _mood = MoodState.Happy;
        else if (Happiness >= (int)MoodState.Content) _mood = MoodState.Content;
        else if (Happiness >= (int)MoodState.Bored) _mood = MoodState.Bored;
        else if (Happiness >= (int)MoodState.Unhappy) _mood = MoodState.Unhappy;
        else if (Happiness >= (int)MoodState.Sad) _mood = MoodState.Sad;
        else _mood = MoodState.Miserable;
    }
    private static void SetStomachState()
    {
        var now = DateTime.UtcNow;
        var timeBetweenLastMeal = now - _lastFed;
        if (timeBetweenLastMeal.TotalMinutes >= HungerMinutes) Hunger -= 5;

        if (Hunger < 20)
        {
            Health -= 1;
            Happiness -= 1;
        }
        else if (Hunger == 0) 
        {
            Health -= 10; 
            Happiness -= 10;
        }


        if (Hunger >= (int)HungerState.Full) _appetite = HungerState.Full;
        else if (Hunger >= (int)HungerState.Satisfied) _appetite = HungerState.Satisfied;
        else if (Hunger >= (int)HungerState.Peckish) _appetite = HungerState.Peckish;
        else if (Hunger >= (int)HungerState.Hungry) _appetite = HungerState.Hungry;
        else if (Hunger >= (int)HungerState.Starving) _appetite = HungerState.Starving;
        else _appetite = HungerState.Famished;
    }
    private static void SetDirtyState()
    {
        var now = DateTime.UtcNow;
        var timeBetweenLastBath = now - _lastBathed;
        if (timeBetweenLastBath.TotalMinutes >= CleanlinessMinutes) Cleanliness -= 5;
        if (Cleanliness < 20) Happiness -= 2;
        else if (Cleanliness == 0) Happiness -= 5;

        if (Cleanliness >= (int)HygienicState.Pristine) _hygiene = HygienicState.Pristine;
        else if (Cleanliness >= (int)HygienicState.Clean) _hygiene = HygienicState.Clean;
        else if (Cleanliness >= (int)HygienicState.Dusty) _hygiene = HygienicState.Dusty;
        else if (Cleanliness >= (int)HygienicState.Dirty) _hygiene = HygienicState.Dirty;
        else _hygiene = HygienicState.Filthy;
    }

    private static void SetDeathState()
    {
        IsDead = true;
        GlobalTimer.StopStatus();
        var now = DateTime.Now;
        TimeLived = now - TimeOfBirth;
        OnDeath?.Invoke();

        var tamagotchiBackup = FilePath.Get("BackUp", "tbkp.dat");
        var eggBackup = FilePath.Get("BackUp", "ebkp.dat");

        if (File.Exists(eggBackup)) File.Delete(eggBackup);
        
        if (File.Exists(tamagotchiBackup)) File.Delete(tamagotchiBackup);
    }

    private static async void Evolve()
    {
        Name = Evolutions.First().Key;
        Id = Evolutions.First().Value.Item1;
        Height = Evolutions.First().Value.Item2;
        Weight = Evolutions.First().Value.Item3;
        Experience = Evolutions.First().Value.Item4;

        await SpritesDownloader.DownloadAsync();

        if (Evolutions.Count > 0)
        {
            var firstKey = Evolutions.First().Key;
            Evolutions.Remove(firstKey);
        }
        OnEvolve?.Invoke();
    }

    public static void UpdateStatus()
    {
        SetMoodState();
        SetStomachState();
        SetDirtyState();
        OnStatusUpdated?.Invoke();
    }
}