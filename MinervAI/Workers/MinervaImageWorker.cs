using MinervAI.Services;
using MinervAI.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MinervAI.Workers{
public class MinervaImageWorker : BackgroundService
{
    private readonly CourseImageService _courseImageService;
    private readonly OpenAIImageService _openAIImageService;
    private readonly ILogger<MinervaImageWorker> _logger;

    public MinervaImageWorker(
        CourseImageService courseImageService,
        OpenAIImageService openAIImageService,
        ILogger<MinervaImageWorker> logger)
    {
        _courseImageService = courseImageService;
        _openAIImageService = openAIImageService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_courseImageService.TryDequeue(out var req, out var prompt))
            {
                _logger.LogInformation(
                    "Generating image in background for course {CourseId}",
                    req.CourseId);

                try
                {
                    var image = await _openAIImageService
                        .GenerateImageAsync(req, prompt);

                    _courseImageService.StoreResult(req.CourseId, image);
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Image generation failed for course {CourseId}",
                        req.CourseId);
                }
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}
}