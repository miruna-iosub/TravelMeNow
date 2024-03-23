using TravelMeNow.Application.Entities;
using Newtonsoft.Json;

namespace TravelMeNow.Application.Models.PlacesDistance;

[Serializable]
public class InfoResponseModel
{
    [JsonProperty("distance")]
    public Distance? NumberOfKilometers { get; set; }

    [JsonProperty("duration")]
    public Duration? EstimatedTime { get; set; }
}