using System.Diagnostics;
using Timer = System.Timers.Timer;

namespace PoketchiCore.Models;
public static class GlobalTimer
{
    private static Timer? _statusTimer;
    private static Timer? _eggTimer;

    public static void Status()
    {
        if (!Egg.IsHatched) return;
        _statusTimer = new Timer(60000);
        _statusTimer.Elapsed += (sender, e) =>
        {
            Tamagotchi.UpdateStatus();
        };
        _statusTimer.AutoReset = true;
        _statusTimer.Start();
    }
    public static void HatchingEgg()
    {
        var timeLeft = Egg.TimeRemaining;

        if (timeLeft <= TimeSpan.Zero)
        {
            Egg.Hatch();
            return;
        }

        _eggTimer = new Timer(timeLeft.TotalMilliseconds);
        _eggTimer.Elapsed += (sender, e) =>
        {
            Egg.Hatch();
        };
        _eggTimer.AutoReset = false;
        _eggTimer.Start();
    }

    public static void StopHatchingEgg()
    {
        _eggTimer?.Stop();
        _eggTimer= null;
    }

    public static void StopStatus()
    {
        _statusTimer?.Stop();
        _statusTimer= null;
    }
}