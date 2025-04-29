using System.Diagnostics;
using System.Security.Cryptography;
namespace PoketchiCore.Controller;
internal class EncryptClass
{
    public static byte[] EncryptStringToBytes_Aes(string plainText, byte[] key, byte[] iv)
    {
        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
        using (var sw = new StreamWriter(cs)) sw.Write(plainText);
        return ms.ToArray();
    }

    public static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] key, byte[] iv)
    {
        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        using var ms = new MemoryStream(cipherText);
        using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);
        return sr.ReadToEnd();
    }
}