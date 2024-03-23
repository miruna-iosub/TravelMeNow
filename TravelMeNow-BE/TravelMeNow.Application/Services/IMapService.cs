using TravelMeNow.Application.Models.Address;
using TravelMeNow.Application.Models.Place;
using TravelMeNow.Application.Models.PlacesDistance;

namespace TravelMeNow.Application.Services;

public interface IMapService
{
    Task<IEnumerable<PlaceResponseModel>> GetPlacesAsync(PlaceRequestModel placeRequestModel);

    Task<InfoResponseModel> GetDistanceFromUserLocationAsync(DistanceRequestModel distanceRequestModel);

    Task<AddressResponseModel> GetAddressByLongitudinalCoordinatesAsync(AddressRequestModel addressRequestModel);

}
