using TravelMeNow.Application.Models.Address;
using TravelMeNow.Application.Models.Place;
using TravelMeNow.Application.Models.PlacesDistance;
using TravelMeNow.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace TravelMeNow.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MapsController : ControllerBase
{
    private readonly IMapService _mapService;
    public MapsController(IMapService mapService)
    {
        _mapService = mapService;
    }

    [HttpGet]
    [Route("places")]
    public async Task<IActionResult> GetPlacesAsync([FromQuery] PlaceRequestModel placeRequestModel)
    {
        var landmark = await _mapService.GetPlacesAsync(placeRequestModel);
        return Ok(landmark);
    }


    [HttpGet]
    [Route("distance")]
    public async Task<IActionResult> GetDistanceAsync([FromQuery] DistanceRequestModel distanceRequestModel)
    {
            var distance = await _mapService.GetDistanceFromUserLocationAsync(distanceRequestModel);
            return Ok(distance);
    }

    [HttpGet]
    [Route("address")]
    public async Task<IActionResult> GetAddressAsync([FromQuery] AddressRequestModel addressRequestModel)
    {
            var address = await _mapService.GetAddressByLongitudinalCoordinatesAsync(addressRequestModel);
            return Ok(address);
    }
}

