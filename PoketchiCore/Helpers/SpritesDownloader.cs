using PoketchiCore.Models;
namespace PoketchiCore.Controller;
internal class SpritesDownloader
{
    private static readonly HttpClient client = new HttpClient();
    public static async Task DownloadAsync()
    {
        var filePath = FilePath.Get("Sprites","sprite.gif");
        int? currentPokemonId = Tamagotchi.Id;
        string url = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/showdown/{currentPokemonId}.gif";
        if (currentPokemonId == null) return;

        try
        {
            byte [] imageBytes = await client.GetByteArrayAsync(url);
            await File.WriteAllBytesAsync(filePath, imageBytes);
        }
        catch (Exception e )
        {
            
            throw new Exception($"Erro ao baixar a imagem: {e.Message}");
        }
    }
}