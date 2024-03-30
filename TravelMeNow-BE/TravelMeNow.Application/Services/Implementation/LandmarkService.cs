using AutoMapper;
using TravelMeNow.Application.Models.Landmark;
using TravelMeNow.DataAccess.Entities;
using TravelMeNow.DataAccess.Repositories;

namespace TravelMeNow.Application.Services.Implementation;

public class LandmarkService : ILandmarkService
{
    private  ILandmarkRepository _landmarkRepository;
    private IMapper _Imapper;

    public LandmarkService(ILandmarkRepository landmarkRepository, IMapper _Imapper)
    {
        _landmarkRepository = landmarkRepository;
        _Imapper = _Imapper;
    }

    public async Task<LandmarkResponseModel> CreateAsync(LandmarkRequestModel landmarkRequestModel)
    {
        var landmark = _Imapper.Map<Landmark>(landmarkRequestModel);
        var addedLandmark = await _landmarkRepository.CreateAsync(landmark);

        return _Imapper.Map<LandmarkResponseModel>(addedLandmark);
    }

    public async Task<IEnumerable<Landmark>> GetAllAsync()
    {
        var landmark = await _landmarkRepository.GetAllAsync();

        return landmark;
    }

    public async Task<Landmark> GetByNameAsync(string landmarkName)
    {
        var landmark = await _landmarkRepository.GetByNameAsync(landmarkName);
        return landmark;
    }
}
