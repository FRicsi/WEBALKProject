using Microsoft.AspNetCore.Mvc;
using MinervAI.Models;
using MinervAI.Services;
namespace MinervAI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
    private readonly CourseImageService _courseImageService;

    public ImageController(CourseImageService courseImageService)
    {
        _courseImageService = courseImageService;
    }

    [HttpPost("generate")]
    public async Task<IActionResult> GenerateImage([FromBody] ImageGenerationRequest request)
    {
        // TODO: later: query course title from DB via GdeWebAPI
        string fakeCourseName = $"Course #{request.CourseId}";

        var response = await _courseImageService.GenerateImageAsync(fakeCourseName, request.Style);

        return Ok(response);
    }

    [HttpGet("ping")]
    public IActionResult Ping() =>
        Ok("MinervAI API is running!!! ✔️");
}
