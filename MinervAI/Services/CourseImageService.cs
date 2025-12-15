using MinervAI.Models;
using System.Collections.Concurrent;

namespace MinervAI.Services;

public class CourseImageService
{
    private readonly ConcurrentQueue<(ImageGenerationRequest req, string prompt)> _queue
    = new();

    private readonly ConcurrentDictionary<int, ImageGenerationResponse> _cache
    = new();
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
    _queue.Enqueue((req, prompt));
    return Task.CompletedTask;
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
