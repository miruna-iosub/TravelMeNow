
using AutoMapper;
using TravelMeNow.Application.Models.Schedule;
using TravelMeNow.DataAccess.Entities;

namespace TravelMeNow.Application.Mapping;

public class ScheduleProfile: Profile
{
    public ScheduleProfile()
    {
        CreateMap<ScheduleRequestModel, Schedule> ();
        CreateMap<Schedule, ScheduleResponseModel> ();
    }
}
