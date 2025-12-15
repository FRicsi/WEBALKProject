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

    public OpenAIImageService(
        HttpClient http,
        IOptions<OpenAISettings> options)
    {
        _http = http;
        _settings = options.Value;
    }

    public async Task<ImageGenerationResponse> GenerateImageAsync(
        ImageGenerationRequest req,
        string prompt)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(_settings.ApiKey))
                throw new InvalidOperationException("OpenAI API key not configured.");

            var requestBody = new
            {
                model = "gpt-image-1",
                prompt = prompt,
                size = "1024x1024"
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _settings.ApiKey);

            var response = await _http.PostAsync(
                "https://api.openai.com/v1/images/generations",
                content);

            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(stream);

            var base64 =
                doc.RootElement
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
        catch (Exception ex)
        {
            // fallback – a rendszer NEM HAL MEG
            return new ImageGenerationResponse
            {
                Base64Image = CourseImageService.TransparentPixel,
                PromptUsed = prompt + " (fallback)",
                Style = req.Style
            };
        }
    }
}
