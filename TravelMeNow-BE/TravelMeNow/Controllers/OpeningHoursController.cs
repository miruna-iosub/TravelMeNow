using TravelMeNow.Application.Models.OpeningHour;
using TravelMeNow.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TravelMeNow.API.Controllers;

[Route("[controller]")]
[ApiController]
public class OpeningHoursController : ControllerBase
{
    private readonly IOpeningHourService _openingHourService;

    public OpeningHoursController(IOpeningHourService openingHourService)
    {
        _openingHourService = openingHourService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody]OpeningHourRequestModel openingHourRequestModel)
    {
        var result = await _openingHourService.CreateAsync(openingHourRequestModel);
        
        return Ok(result);
    }
}
