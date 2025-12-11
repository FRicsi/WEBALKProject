using Microsoft.Extensions.Hosting;
using MinervAI.Services;

namespace MinervAI.Workers;

public class MinervaImageWorker : BackgroundService
{
    private readonly IServiceProvider _provider;

    public MinervaImageWorker(IServiceProvider provider)
    {
        _provider = provider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // TODO: Scheduled background image generation
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(5000, stoppingToken);
        }
    }
}

