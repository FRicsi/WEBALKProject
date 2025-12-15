using MinervAI.Models;

namespace MinervAI.Services;

public class ImagePromptBuilderService
{
    public string BuildPrompt(ImageGenerationRequest req)
    {
        var style = ImageStyles.Normalize(req.Style);

        return 
            $"Illustration for course '{req.CourseTitle}'. " +
            $"Description: {req.Description}. " +
            $"Style: {style}. " +
            "Clean, modern, educational composition.";
    }
}
