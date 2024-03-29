using AutoMapper;
using TravelMeNow.Application.Models.Landmark;
using TravelMeNow.DataAccess.Entities;
using TravelMeNow.DataAccess.Repositories;

namespace TravelMeNow.Application.Services.Impl;

public class LandmarkService : ILandmarkService
{
    private readonly ILandmarkRepository _landmarkRepository;
    private readonly IMapper _mapper;

    public LandmarkService(ILandmarkRepository landmarkRepository, IMapper mapper)
    {
        _landmarkRepository = landmarkRepository;
        _mapper = mapper;
    }

    public async Task<LandmarkResponseModel> CreateAsync(LandmarkRequestModel landmarkRequestModel)
    {
        var landmark = _mapper.Map<Landmark>(landmarkRequestModel);
        var addedLandmark = await _landmarkRepository.CreateAsync(landmark);

        return _mapper.Map<LandmarkResponseModel>(addedLandmark);
    }

    public async Task<IEnumerable<Landmark>> GetAllAsync()
    {
        var landmark = await _landmarkRepository.GetAllAsync();

        return landmark;
    }

    public async Task<Landmark> GetByNameAsync(string name)
    {
        var landmark = await _landmarkRepository.GetByNameAsync(name);
        return landmark;
    }
}
