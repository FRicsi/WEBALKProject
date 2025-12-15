using MinervAI.Models;
using MinervAI.Services;
using MinervAI.Workers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<OpenAISettings>(
    builder.Configuration.GetSection("OpenAI"));

builder.Services.AddHttpClient("OpenAI", client =>
{
    client.BaseAddress = new Uri("https://api.openai.com/v1/");
});

builder.Services.AddControllers();

// Services
/*builder.Services.AddScoped<ImagePromptBuilderService>();
builder.Services.AddScoped<CourseImageService>();*/
builder.Services.AddSingleton<CourseImageService>();
builder.Services.AddSingleton<ImagePromptBuilderService>();
builder.Services.AddSingleton<OpenAIImageService>();

// Background worker
builder.Services.AddHostedService<MinervaImageWorker>();

var app = builder.Build();

app.MapControllers();

app.Run();
