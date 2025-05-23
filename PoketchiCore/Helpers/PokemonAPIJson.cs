﻿using System.Text.Json;
namespace PoketchiCore.Controller;
internal class PokemonAPIJson
{
    private static readonly HttpClient httpClient = new HttpClient();
    public static async Task<JsonElement> GetAsync(string url,int id)
    {
        string endpoint = $"{url}{id}";
        var response = await httpClient.GetStringAsync(endpoint);
        using var doc = JsonDocument.Parse(response);
        return doc.RootElement.Clone();
    }
}