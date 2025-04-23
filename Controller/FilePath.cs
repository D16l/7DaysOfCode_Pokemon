namespace _7DaysOfCode_Pokemon.Controller;
internal class FilePath
{
    public static string Get(string subfolder)
    {
        var baseDir = AppContext.BaseDirectory;
        string projectDir = Directory.GetParent(baseDir)!.Parent!.Parent!.Parent!.FullName;
        string folderPath = Path.Combine(projectDir, subfolder);

        if (!Directory.Exists(projectDir))
        {
            Directory.CreateDirectory(projectDir);
        }

        return Path.Combine(folderPath, "sprite.gif");
    }
}
