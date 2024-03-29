using Newtonsoft.Json;

namespace TravelMeNow.Application.Models.GoogleLocation;

[Serializable]
public class ApiResultResponseModel
{
    [JsonProperty("results")]
    public IEnumerable<GoogleLocationResponseModel>? GoogleLocations { get; set; }
}
