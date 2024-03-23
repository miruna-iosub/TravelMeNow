using AutoMapper;
using TravelMeNow.Application.Models.OpeningHour;
using TravelMeNow.DataAccess.Entities;
using TravelMeNow.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelMeNow.Application.Services.Impl;

public class OpeningHourService : IOpeningHourService
{
    private readonly IOpeningHourRepository _openingHourRepository;
    private readonly IMapper _mapper;

    public OpeningHourService(IOpeningHourRepository openingHourRepository, IMapper mapper)
    {
        _openingHourRepository = openingHourRepository;
        _mapper = mapper;
    } 

    public async Task<OpeningHourResponseModel> CreateAsync(OpeningHourRequestModel openingHourRequestModel)
    {
        var openingHour = _mapper.Map<OpeningHour>(openingHourRequestModel);
        var addedOpeningHour = await _openingHourRepository.CreateAsync(openingHour);
        return _mapper.Map<OpeningHourResponseModel>(addedOpeningHour);
    }
}
