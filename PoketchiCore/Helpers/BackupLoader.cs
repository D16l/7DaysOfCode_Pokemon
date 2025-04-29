using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace PoketchiCore.Controller;
internal class BackupLoader
{
    private static readonly byte[] Key = Convert.FromBase64String(Environment.GetEnvironmentVariable("ENCRYPT_KEY") ?? "");
    private static readonly byte[] IV = Convert.FromBase64String(Environment.GetEnvironmentVariable("ENCRYPT_IV") ?? "");

    public static void Save<T>(T obj, string fileName)
    {

        var filePath = FilePath.Get("BackUp", fileName);
        var json = JsonSerializer.Serialize(obj, new JsonSerializerOptions
        {
            IncludeFields = true,
            WriteIndented = false
        });

        var encrypted = EncryptClass.EncryptStringToBytes_Aes(json, Key, IV);
        File.WriteAllBytes(filePath, encrypted);
    }

    public static T? Load<T>(string fileName) where T : class
    {
        var filePath = FilePath.Get("BackUp", fileName);
        if (!File.Exists(filePath)) return null;

        var encrypted = File.ReadAllBytes(filePath);
        var json = EncryptClass.DecryptStringFromBytes_Aes(encrypted, Key, IV);

        return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
        {
            IncludeFields = true,
            PropertyNameCaseInsensitive = true
        });
    }
}