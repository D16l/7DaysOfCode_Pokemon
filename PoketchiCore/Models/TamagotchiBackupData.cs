namespace PoketchiCore.Models;
public class TamagotchiBackupData
{
    public DateTime TimeOfBirth { get; set; }
    public bool IsDead { get; set; }
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public int Health {  get; set; }
    public int Happiness {  get; set; }
    public int Hunger {  get; set; }
    public int Cleanliness {  get; set; }
    public int Experience {  get; set; }
    public int HealCharges {  get; set; }
    public MoodState Mood { get; set; }
    public HungerState Appetite { get; set; }
    public HygienicState Hygiene {  get; set; }
    public DateTime _lastHealRegen {  get; set; }
    public DateTime _lastPlayed {  get; set; }
    public DateTime _lastBathed {  get; set; }

    public Dictionary<string, Tuple<int, int, int, int>> Evolutions = [];
}