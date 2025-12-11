using MinervAI.Models;
using Microsoft.Extensions.Options;

namespace MinervAI.Services;

public class OpenAiImageService
{
    private readonly OpenAiSettings _settings;

    public OpenAiImageService(IOptions<OpenAiSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task<string> GenerateImageBase64Async(string prompt)
    {
        // TODO: OpenAI API integration
        await Task.Delay(200); 
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"Fake image: {prompt}"));
    }
}
