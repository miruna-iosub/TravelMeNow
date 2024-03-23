using TravelMeNow.Application.Exceptions;
using TravelMeNow.Application.Models.Address;
using TravelMeNow.Application.Models.Place;
using TravelMeNow.Application.Models.PlacesDistance;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace TravelMeNow.Application.Services.Impl;


public class MapService : IMapService
{
    private readonly HttpClient _httpClient;
    public MapService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<PlaceResponseModel>> GetPlacesAsync(PlaceRequestModel placeRequestModel)
    {
        var pois = new List<PlaceResponseModel>();

        var key = "GoogleMapsKey";
        Console.WriteLine(key);
        foreach (var keyword in placeRequestModel.Query.Split(','))
        {
            var url = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?keyword={keyword}&location={placeRequestModel.Latitude},{placeRequestModel.Longitude}&rankby=distance&key={key}";
            
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {                   
                var jsonString = await response.Content.ReadAsStringAsync();
                var placesList = JsonConvert.DeserializeObject<ResultResponseModel>(jsonString);

                if (placesList != null && placesList.Places != null)
                {
                    pois.AddRange(placesList.Places);
                }
            }
        }

        return pois;
    }

    public async Task<InfoResponseModel> GetDistanceFromUserLocationAsync(DistanceRequestModel distanceRequestModel)
    {
        var key = "GoogleMapsKey";
        Console.WriteLine(key);
        var url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={distanceRequestModel.OriginLatitude}, {distanceRequestModel.OriginLongitude}&destinations={distanceRequestModel.DestLatitude}, {distanceRequestModel.DestLongitude}&mode=walking&key={key}";

        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var distancesList = JsonConvert.DeserializeObject<RowResponseModel>(jsonString);

            if (distancesList != null)
            {
                var info = distancesList.Distances.First().Infos.First();
                return info;
            }
        }
        throw new DistanceException("Some error occured while calling Google Maps Distance API");
    }

    public async Task<AddressResponseModel> GetAddressByLongitudinalCoordinatesAsync(AddressRequestModel addressRequestModel)
    {
        var key = "GoogleMapsKey";

        var url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={addressRequestModel.Latitude}, {addressRequestModel.Longitude}&key={key}&result_type=route";

        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var addresses = JsonConvert.DeserializeObject<ApiResultResponseModel>(jsonString);
            if (addresses != null)
            {
                var address = addresses.Addresses.First();
                return address;
            }
        }
        throw new AddressException("Some error occured while calling Google Maps Geocoding API");
    }
}
