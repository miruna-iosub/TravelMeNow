using AutoMapper;
using TravelMeNow.Application.Exceptions;
using TravelMeNow.Application.Models.PointOfInterest;
using TravelMeNow.DataAccess.Entities;
using TravelMeNow.DataAccess.Repositories;

namespace TravelMeNow.Application.Services.Impl;

public class PointOfInterestService : IPointOfInterestService
{
    private readonly IPointOfInterestRepository _pointOfInterestRepository;
    private readonly IMapper _mapper;

    public PointOfInterestService(IPointOfInterestRepository pointOfInterestRepository, IMapper mapper)
    {
        _pointOfInterestRepository = pointOfInterestRepository;
        _mapper = mapper;
    }

    public async Task<PointOfInterestResponseModel> CreateAsync(PointOfInterestRequestModel pointOfInterestRequestModel)
    {
        var pointOfInterest = _mapper.Map<PointOfInterest>(pointOfInterestRequestModel);
        var addedPointOfInterest = await _pointOfInterestRepository.CreateAsync(pointOfInterest);

        return _mapper.Map<PointOfInterestResponseModel>(addedPointOfInterest);
    }

    public async Task<IEnumerable<PointOfInterest>> GetAllAsync()
    {
        var pointsOfInterest = await _pointOfInterestRepository.GetAllAsync();

        return pointsOfInterest;
    }

    public async Task<PointOfInterest> GetByNameAsync(string name)
    {
        var pointOfInterest = await _pointOfInterestRepository.GetByNameAsync(name);
        if (pointOfInterest == null) { throw new PointOfInterestNotFoundException(name); }
        return pointOfInterest;
    }
}
