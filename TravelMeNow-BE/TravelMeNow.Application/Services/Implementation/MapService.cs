using TravelMeNow.Application.Models.GoogleLocation;
using TravelMeNow.Application.Models.Spot;
using TravelMeNow.Application.Models.SpotsGap;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace TravelMeNow.Application.Services.Implementation;


public class MapService : IMapService
{
    private HttpClient _httpClient;
    public MapService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<SpotResponseModel>> GetSpotsAsync(SpotRequestModel spotRequestModel)
    {
        var landmark = new List<SpotResponseModel>();

        var key = "AIzaSyAq1SLR2yidSfrt9TSPNQadb1PuQqh2x_Y";
        var landmarks = new List<SpotResponseModel>();

        foreach (var keyword in spotRequestModel.Query.Split(','))
        {
            var url = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?keyword={keyword.Trim()}&location={spotRequestModel.Lat},{spotRequestModel.Long}&rankby=distance&key={key}";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var spotsList = JsonConvert.DeserializeObject<ResultResponseModel>(jsonString);

                    if (spotsList?.Spots != null)
                    {
                        landmarks.AddRange(spotsList.Spots);
                    }
                }
                else
                {
                    Console.WriteLine($"Error fetching data: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        return landmarks;
    }

    public async Task<InfoResponseModel> GetGapFromUserLocationAsync(GapRequestModel gapRequestModel)
    {
        var key = "AIzaSyAq1SLR2yidSfrt9TSPNQadb1PuQqh2x_Y";
        var url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={gapRequestModel.OriginLat}, {gapRequestModel.OriginLong}&destinations={gapRequestModel.DestLat}, {gapRequestModel.DestLong}&mode=walking&key={key}";

        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var gapsList = JsonConvert.DeserializeObject<RowResponseModel>(jsonString);

            if (gapsList != null)
            {
                var details = gapsList.Gaps.First().Details.First();
                return details;
            }
        }
        throw new InvalidOperationException("No gaps found.");

    }

    public async Task<GoogleLocationResponseModel> GetGoogleLocationByLongCoordinatesAsync(GoogleLocationRequestModel googleLocationRequestModel)
    {
        var key = "AIzaSyAq1SLR2yidSfrt9TSPNQadb1PuQqh2x_Y";

        var url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={googleLocationRequestModel.Lat}, {googleLocationRequestModel.Long}&key={key}&result_type=route";

        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var locations = JsonConvert.DeserializeObject<RestApiResultResponseModel>(jsonString);
            if (locations != null)
            {
                var location = locations.GoogleLocations.First();
                return location;
            }
        }
        throw new InvalidOperationException("No locations found.");

    }
}
