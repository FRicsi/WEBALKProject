using Microsoft.AspNetCore.Mvc;
using MinervAI.Models;
using MinervAI.Services;

namespace MinervAI.Controller;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
    private readonly ImagePromptBuilderService _promptService;
    private readonly CourseImageService _courseImageService;

    public ImageController(
        ImagePromptBuilderService promptService,
        CourseImageService courseImageService)
    {
        _promptService = promptService;
        _courseImageService = courseImageService;
    }

    [HttpGet("ping")]
    public IActionResult Ping() => Ok("Image API OK");

    [HttpPost("generate")]
    public async Task<IActionResult> Generate([FromBody] ImageGenerationRequest request)
    {
        var prompt = _promptService.BuildPrompt(request);
        var result = await _courseImageService.GenerateImageAsync(request, prompt);

        return Ok(result);
    }
}