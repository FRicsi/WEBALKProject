namespace MinervAI.Models;

public class ImageGenerationRequest
{
    public int CourseId { get; set; }
    public string? CourseTitle { get; set; }
    public string? Description { get; set; }
    public string? Style { get; set; }
}
