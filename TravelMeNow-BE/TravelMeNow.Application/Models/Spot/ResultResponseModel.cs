using Newtonsoft.Json;

namespace TravelMeNow.Application.Models.Spot;

[Serializable]
public class ResultResponseModel
{
    [JsonProperty("results")]
    public IEnumerable<SpotResponseModel>? Spots { get; set; }
}
