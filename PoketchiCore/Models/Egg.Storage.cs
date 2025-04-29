using PoketchiCore.Controller;
using System.Diagnostics;

namespace PoketchiCore.Models;
public partial class Egg
{
    public static void Storage(StorageMode mode)
    {
        if (mode == StorageMode.Save)
        {
            var data = new EggBackupData
            {
                HatchStartTime = HatchStartTime,
                Created = Created,
                IsHatched = IsHatched
            };
            BackupLoader.Save(data, "ebkp.dat");
        }
        else if (mode == StorageMode.Load)
        {
            var data = BackupLoader.Load<EggBackupData>("ebkp.dat");

            if (data != null) 
            {
                Created = data.Created;
                IsHatched = data.IsHatched;
                HatchStartTime = data.HatchStartTime;
            }
        }
    }
    public static void CheckHatchAfterLoad()
    {
        if (Created && !IsHatched && TimeRemaining <= TimeSpan.Zero)
        {
            Hatch();
        }
        else if (IsHatched)
        {
            OnHatched?.Invoke();
        }
        else
        {
            GlobalTimer.HatchingEgg();
        }
    }
}