using TravelMeNow.Application.Models.GoogleLocation;
using TravelMeNow.Application.Models.Spot;
using TravelMeNow.Application.Models.SpotsGap;

namespace TravelMeNow.Application.Services;

public interface IMapService
{
    Task<IEnumerable<SpotResponseModel>> GetSpotsAsync(SpotRequestModel spotRequestModel);

    Task<InfoResponseModel> GetGapFromUserLocationAsync(GapRequestModel gapRequestModel);

    Task<GoogleLocationResponseModel> GetGoogleLocationByLongCoordinatesAsync(GoogleLocationRequestModel googleLocationRequestModel);
}
