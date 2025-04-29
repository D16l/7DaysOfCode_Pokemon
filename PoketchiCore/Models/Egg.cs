using PoketchiCore.Controller;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace PoketchiCore.Models;
public partial class Egg
{
    public static DateTime HatchStartTime { get; private set; }

    private static readonly TimeSpan HatchDuration = TimeSpan.FromMinutes(1);
    public static TimeSpan TimeRemaining =>
    TimeSpan.FromTicks(Math.Max((HatchDuration - (DateTime.UtcNow - HatchStartTime)).Ticks, 0));
    public static bool IsHatched { get; private set; } = false;

    public static event Action? OnHatched;
    public static bool Created { get; private set; } = false;
    public static async Task Create()
    {
        Debug.WriteLine("criei");
        if (Tamagotchi.IsDead) Created = false;
        if (Created) return;
        IsHatched = false;
        HatchStartTime = DateTime.UtcNow;

        await Pokemon.CreatePokemonAsync();
        await SpritesDownloader.DownloadAsync();
        Created = true;
        GlobalTimer.HatchingEgg();
    }
    public static void Hatch()
    {
        if (Created == false) return;
        if (IsHatched) return;
        IsHatched = true;
        OnHatched?.Invoke();
        GlobalTimer.Status();
    }
}