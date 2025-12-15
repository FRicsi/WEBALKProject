namespace MinervAI.Models;

public class ImageGenerationResponse
{
    public string Base64Image { get; set; } = string.Empty;
    public string PromptUsed { get; set; } = string.Empty;
    public string? Style { get; set; }
}
