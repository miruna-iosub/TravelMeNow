using TravelMeNow.Application.Models.Schedule;
using TravelMeNow.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TravelMeNow.API.Controllers;

[Route("[controller]")]
[ApiController]
public class SchedulesController : ControllerBase
{
    private IScheduleService _scheduleService;

    public SchedulesController(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody]ScheduleRequestModel scheduleRequestModel)
    {
        var result = await _scheduleService.CreateAsync(scheduleRequestModel);
        
        return Ok(result);
    }
}
