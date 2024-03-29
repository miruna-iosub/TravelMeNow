using TravelMeNow.Application.Entities;
using Newtonsoft.Json;

namespace TravelMeNow.Application.Models.SpotsGap;

[Serializable]
public class InfoResponseModel
{
    [JsonProperty("duration")]
    public Duration? eta { get; set; }
}