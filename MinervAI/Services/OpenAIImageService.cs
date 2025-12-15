using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using MinervAI.Models;

namespace MinervAI.Services;

public class OpenAIImageService
{
    private readonly HttpClient _http;
    private readonly OpenAISettings _settings;
    private readonly ILogger<OpenAIImageService> _logger;

    public OpenAIImageService(
        IHttpClientFactory factory,
        IOptions<OpenAISettings> settings,
        ILogger<OpenAIImageService> logger)
    {
        _http = factory.CreateClient("OpenAI");
        _settings = settings.Value;
        _logger = logger;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _settings.ApiKey);
    }

    public async Task<ImageGenerationResponse> GenerateImageBase64Async(
        ImageGenerationRequest req,
        string prompt,
        CancellationToken ct)
    {
        var payload = new
        {
            model = _settings.Model,          // gpt-image-1
            prompt = prompt,
            size = _settings.ImageSize,       // 1024x1024
            /*response_format = "b64_json"*/
        };

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _http.PostAsync(
            "images/generations",
            content,
            ct);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(ct);
            _logger.LogError("OpenAI error response: {Error}", error);
            throw new HttpRequestException(error);
        }

        using var stream = await response.Content.ReadAsStreamAsync(ct);
        using var doc = await JsonDocument.ParseAsync(stream, cancellationToken: ct);

        var base64 = doc.RootElement
            .GetProperty("data")[0]
            .GetProperty("b64_json")
            .GetString();

        return new ImageGenerationResponse
        {
            Base64Image = base64!,
            PromptUsed = prompt,
            Style = req.Style
        };
    }
}
