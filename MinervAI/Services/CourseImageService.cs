using MinervAI.Models;

namespace MinervAI.Services;

public class CourseImageService
{
    private const string TransparentPixel =
        "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR4nGNgYAAAAAMAASsJTYQAAAAASUVORK5CYII=";

    private readonly OpenAIImageService _openAIImageService;

    public CourseImageService(OpenAIImageService openAIImageService)
    {
        _openAIImageService = openAIImageService;
    }

    public async Task<ImageGenerationResponse> GenerateImageAsync(
        ImageGenerationRequest req, string prompt)
    {
        try
        {
            // 👉 Itt hívjuk meg a valódi OpenAI-t
            var base64 = await _openAIImageService
                .GenerateImageBase64Async(prompt);

            return new ImageGenerationResponse
            {
                Base64Image = base64,
                PromptUsed = prompt,
                Style = req.Style
            };
        }
        catch
        {
            // 👉 Ha bármi elromlik, visszaesünk fake képre
            return new ImageGenerationResponse
            {
                Base64Image = TransparentPixel,
                PromptUsed = prompt,
                Style = req.Style
            };
        }
    }
}
