using MinervAI.Models;
using System.Collections.Concurrent;

namespace MinervAI.Services;

public class CourseImageService
{
    public const string TransparentPixel =
        "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR4nGNgYAAAAAMAASsJTYQAAAAASUVORK5CYII=";

    private readonly ConcurrentQueue<(ImageGenerationRequest req, string prompt)> _queue = new();
    private readonly ConcurrentDictionary<int, ImageGenerationResponse> _cache = new();

    public CourseImageService()
    {
    }
    public Task<ImageGenerationResponse> GenerateImageAsync(
        ImageGenerationRequest req, string prompt)
    {
        _queue.Enqueue((req, prompt));

        // Azonnali válasz: "folyamatban" lesz
        return Task.FromResult(new ImageGenerationResponse
        {
            Base64Image = TransparentPixel,
            PromptUsed = prompt,
            Style = req.Style
        });
    }

    public bool TryDequeue(out ImageGenerationRequest req, out string prompt)
    {
        if (_queue.TryDequeue(out var item))
        {
            req = item.req;
            prompt = item.prompt;
            return true;
        }

        req = null!;
        prompt = null!;
        return false;
    }

    public void StoreResult(int courseId, ImageGenerationResponse result)
    {
        _cache[courseId] = result;
    }

    public bool TryGetResult(int courseId, out ImageGenerationResponse result)
    {
        return _cache.TryGetValue(courseId, out result!);
    }
}
