namespace MinervAI.Services;

public class ImagePromptBuilderService
{
    public string BuildPrompt(string courseName, string style)
    {
        return style switch
        {
            "low-poly" => $"Low-poly illustration of course topic: {courseName}. Stylized geometric look.",
            "futuristic" => $"Futuristic digital art representing the course topic: {courseName}. Neon, sci-fi style.",
            "lapbook" => $"Lapbook-style cute paper cutout illustration of {courseName}.",
            _ => $"Illustration representing the theme of the course: {courseName}."
        };
    }
}
