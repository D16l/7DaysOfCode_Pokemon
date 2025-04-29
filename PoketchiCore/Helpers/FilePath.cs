using System.Diagnostics;

namespace PoketchiCore.Controller;
public class FilePath
{
    public static string Get(string subfolder,string fileName)
    {
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string folderPath = Path.Combine(baseDir, subfolder);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        return Path.Combine(folderPath, fileName);
    }
}