namespace MinervAI.Models;

public static class ImageStyles
{
    public static string Normalize(string? style)
    {
        return style?.ToLower() switch
        {
            "lapbook" => "hand-drawn school lapbook style",
            "futuristic" => "futuristic neon sci-fi illustration",
            "low-poly" => "low poly geometric illustration",
            _ => "detailed artistic digital illustration"
        };
    }
}
