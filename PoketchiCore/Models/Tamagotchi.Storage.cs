using PoketchiCore.Controller;

namespace PoketchiCore.Models;
public partial class Tamagotchi
{
    public static void Storage(StorageMode mode)
    {
        if (mode == StorageMode.Save)
        {
            var data = new TamagotchiBackupData
            {
                TimeOfBirth = TimeOfBirth,
                IsDead = IsDead,
                Id = Id,
                Name = Name,
                Height = Height,
                Weight = Weight,
                Health = Health,
                HealCharges = HealCharges,
                Happiness = Happiness,
                Hunger = Hunger,
                Cleanliness = Cleanliness,
                Experience = Experience,
                Mood = Mood,
                Appetite = Appetite,
                Hygiene = Hygiene,
                _lastBathed = _lastBathed,
                _lastHealRegen = _lastHealRegen,
                _lastPlayed = _lastPlayed,
                Evolutions = Evolutions
            };

            BackupLoader.Save(data, "tbkp.dat");
        }
        else if (mode == StorageMode.Load)
        {
            var data = BackupLoader.Load<TamagotchiBackupData>("tbkp.dat");

            if (data != null) 
            {
                TimeOfBirth = data.TimeOfBirth;
                IsDead = data.IsDead;
                Id = data.Id;
                Name = data.Name;
                Height = data.Height;
                Weight = data.Weight;
                Health = data.Health;
                HealCharges = data.HealCharges;
                Happiness = data.Happiness;
                Hunger = data.Hunger;
                Cleanliness = data.Cleanliness;
                Experience = data.Experience;
                _mood = data.Mood;
                _appetite = data.Appetite;
                _hygiene = data.Hygiene;
                _lastBathed = data._lastBathed;
                _lastHealRegen = data._lastHealRegen;
                _lastPlayed = data._lastPlayed;
                Evolutions = data.Evolutions;
            }
        }
    }
}
