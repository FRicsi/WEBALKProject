using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using MinervAI.Models;

namespace MinervAI.Services;

public class OpenAIImageService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly OpenAISettings _settings;

    public OpenAIImageService(
        IHttpClientFactory httpClientFactory,
        IOptions<OpenAISettings> settings)
    {
        _httpClientFactory = httpClientFactory;
        _settings = settings.Value;
    }

    public async Task<string> GenerateImageBase64Async(string prompt)
    {
        var client = _httpClientFactory.CreateClient("OpenAI");

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _settings.ApiKey);

        var request = new
        {
            model = _settings.Model,
            prompt = prompt,
            size = _settings.ImageSize
        };

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("images/generations", content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("OpenAI image generation failed");
        }

        var responseJson = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(responseJson);

        return doc.RootElement
            .GetProperty("data")[0]
            .GetProperty("b64_json")
            .GetString()!;
    }
}