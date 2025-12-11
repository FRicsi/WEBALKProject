namespace MinervAI.Models;

public class ImageGenerationResponse
{
    public bool Success { get; set; }
    public string? ImageBase64 { get; set; }
    public string? Style { get; set; }
    public string? ErrorMessage { get; set; }
}