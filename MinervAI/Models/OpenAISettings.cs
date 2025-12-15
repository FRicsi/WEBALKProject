namespace MinervAI.Models;

public class OpenAISettings
{
    public string ApiKey { get; set; } = string.Empty;
    public string Model { get; set; } = "gpt-image-1";
    public string ImageSize { get; set; } = "1024x1024";
}
