using TravelMeNow.Application.Models.Landmark;
using TravelMeNow.DataAccess.Entities;

namespace TravelMeNow.Application.Services;

public interface ILandmarkService
{
    Task<LandmarkResponseModel> CreateAsync(LandmarkRequestModel landmark);

    Task<IEnumerable<Landmark>> GetAllAsync();

    Task<Landmark> GetByNameAsync(string landmarkName);
}

