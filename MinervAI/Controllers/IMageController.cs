using Microsoft.AspNetCore.Mvc;
using MinervAI.Models;
using MinervAI.Services;

namespace MinervAI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
    private readonly CourseImageService _courseImageService;
    private readonly ImagePromptBuilderService _promptBuilder;
    private readonly ILogger<ImageController> _logger;

    public ImageController(
        CourseImageService courseImageService,
        ImagePromptBuilderService promptBuilder,
        ILogger<ImageController> logger)
    {
        _courseImageService = courseImageService;
        _promptBuilder = promptBuilder;
        _logger = logger;
    }

    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok("Image API OK");
    }

    [HttpPost("generate")]
    public async Task<IActionResult> Generate(ImageGenerationRequest req)
    {
        try
        {
            var prompt = _promptBuilder.BuildPrompt(req);
            var result = await _courseImageService.GenerateImageAsync(req, prompt);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Image generation failed");
            return StatusCode(500, ex.Message);
        }
    }
}
