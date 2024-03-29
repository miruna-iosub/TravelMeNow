using AutoMapper;
using TravelMeNow.Application.Models.Schedule;
using TravelMeNow.DataAccess.Entities;
using TravelMeNow.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelMeNow.Application.Services.Impl;

public class ScheduleService : IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IMapper _mapper;

    public ScheduleService(IScheduleRepository scheduleRepository, IMapper mapper)
    {
        _scheduleRepository = scheduleRepository;
        _mapper = mapper;
    } 

    public async Task<ScheduleResponseModel> CreateAsync(ScheduleRequestModel scheduleRequestModel)
    {
        var schedule = _mapper.Map<Schedule>(scheduleRequestModel);
        var addedSchedule = await _scheduleRepository.CreateAsync(schedule);
        return _mapper.Map<ScheduleResponseModel>(addedSchedule);
    }
}
