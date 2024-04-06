using AutoMapper;
using TravelMeNow.Application.Models.Schedule;
using TravelMeNow.DataAccess.Entities;
using TravelMeNow.DataAccess.Repositories;

namespace TravelMeNow.Application.Services.Implementation;

public class ScheduleService : IScheduleService
{
    private IScheduleRepository _scheduleRepository;
    private IMapper _Imapper;

    public ScheduleService(IScheduleRepository scheduleRepository, IMapper mapper)
    {
        _scheduleRepository = scheduleRepository;
        _Imapper = mapper;
    } 

    public async Task<ScheduleResponseModel> CreateAsync(ScheduleRequestModel scheduleRequestModel)
    {
        var schedule = _Imapper.Map<Schedule>(scheduleRequestModel);
        var addedSchedule = await _scheduleRepository.CreateAsync(schedule);
        return _Imapper.Map<ScheduleResponseModel>(addedSchedule);
    }
}
