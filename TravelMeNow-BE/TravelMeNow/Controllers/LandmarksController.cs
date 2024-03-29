using TravelMeNow.Application.Models.Landmark;
using TravelMeNow.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace TravelMeNow.API.Controllers;

[ApiController]
[Route("[controller]")]
public class LandmarksController : ControllerBase
{
    private readonly ILandmarkService _landmarkService;

    public LandmarksController(ILandmarkService landmarkService)
    {
        _landmarkService = landmarkService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] LandmarkRequestModel landmark)
    {
        var result = await _landmarkService.CreateAsync(landmark);
        return Ok(result);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetByNameAsync(string name)
    {
            var result = await _landmarkService.GetByNameAsync(name);
            return Ok(result);

    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _landmarkService.GetAllAsync();

        return Ok(result);
    }
}
