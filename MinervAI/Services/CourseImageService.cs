using MinervAI.Models;

namespace MinervAI.Services;

public class CourseImageService
{
    private readonly ImagePromptBuilderService _promptBuilder;
    private readonly OpenAiImageService _aiService;

    public CourseImageService(
        ImagePromptBuilderService promptBuilder,
        OpenAiImageService aiService)
    {
        _promptBuilder = promptBuilder;
        _aiService = aiService;
    }

    public async Task<ImageGenerationResponse> GenerateImageAsync(string courseName, string style)
    {
        var prompt = _promptBuilder.BuildPrompt(courseName, style);
        var base64 = await _aiService.GenerateImageBase64Async(prompt);

        return new ImageGenerationResponse
        {
            Success = true,
            Style = style,
            ImageBase64 = base64
        };
    }
}

