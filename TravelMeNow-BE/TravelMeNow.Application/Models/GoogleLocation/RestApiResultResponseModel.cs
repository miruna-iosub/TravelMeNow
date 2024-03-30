using Newtonsoft.Json;

namespace TravelMeNow.Application.Models.GoogleLocation;

[Serializable]
public class RestApiResultResponseModel
{
    [JsonProperty("results")]
    public IEnumerable<GoogleLocationResponseModel>? GoogleLocations { get; set; }
}
