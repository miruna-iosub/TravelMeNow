using TravelMeNow.Application.Models.GoogleLocation;
using TravelMeNow.Application.Models.Spot;
using TravelMeNow.Application.Models.SpotsGap;
using TravelMeNow.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace TravelMeNow.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MapsController : ControllerBase
{
    private IMapService _mapService;
    public MapsController(IMapService mapService)
    {
        _mapService = mapService;
    }

    [HttpGet]
    [Route("spots")]
    public async Task<IActionResult> GetSpotsAsync([FromQuery] SpotRequestModel spotRequestModel)
    {
        var landmark = await _mapService.GetSpotsAsync(spotRequestModel);
        return Ok(landmark);
    }


    [HttpGet]
    [Route("gap")]
    public async Task<IActionResult> GetGapAsync([FromQuery] GapRequestModel gapRequestModel)
    {
        var gap = await _mapService.GetGapFromUserLocationAsync(gapRequestModel);
        return Ok(gap);
    }


    [HttpGet]
    [Route("googlelocation")]
    public async Task<IActionResult> GetGoogleLocationAsync([FromQuery] GoogleLocationRequestModel googleLocationRequestModel)
    {
        var googlelocation = await _mapService.GetGoogleLocationByLongCoordinatesAsync(googleLocationRequestModel);
        return Ok(googlelocation);
    }
}

