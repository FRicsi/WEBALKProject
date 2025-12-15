namespace MinervAI.Workers;

public class MinervaImageWorker : BackgroundService
{
    private readonly CourseImageService _courseImageService;
    private readonly ILogger<MinervaImageWorker> _logger;

    public MinervaImageWorker(
        CourseImageService courseImageService,
        ILogger<MinervaImageWorker> logger)
    {
        _courseImageService = courseImageService;
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

                // IDE JÖN MAJD AZ OPENAI HÍVÁS
                var fakeImage = new ImageGenerationResponse
                {
                    Base64Image = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR4nGNgYAAAAAMAASsJTYQAAAAASUVORK5CYII=",
                    PromptUsed = prompt,
                    Style = req.Style
                };

                _courseImageService.StoreResult(req.CourseId, fakeImage);
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}
