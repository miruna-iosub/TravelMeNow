
using TravelMeNow.Application.Models.Schedule;

namespace TravelMeNow.Application.Services;

public interface IScheduleService
{
    public Task<ScheduleResponseModel> CreateAsync(ScheduleRequestModel scheduleRequest);
}
