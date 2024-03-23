using TravelMeNow.Application.Models.PointOfInterest;
using TravelMeNow.DataAccess.Entities;

namespace TravelMeNow.Application.Services;

public interface IPointOfInterestService
{
    Task<PointOfInterestResponseModel> CreateAsync(PointOfInterestRequestModel pointOfInterest);

    Task<PointOfInterest> GetByNameAsync(string name);

    Task<IEnumerable<PointOfInterest>> GetAllAsync();
}

