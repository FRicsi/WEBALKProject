using MinervAI.Models;
using MinervAI.Services;
using MinervAI.Workers;

var builder = WebApplication.CreateBuilder(args);

// OpenAI configuration
builder.Services.Configure<OpenAiSettings>(
    builder.Configuration.GetSection("OpenAI"));

// Register AI services
builder.Services.AddSingleton<ImagePromptBuilderService>();
builder.Services.AddSingleton<OpenAiImageService>();
builder.Services.AddSingleton<CourseImageService>();

// Background Worker
builder.Services.AddHostedService<MinervaImageWorker>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();