using Newtonsoft.Json;

namespace TravelMeNow.Application.Models.SpotsGap;

[Serializable]
public class RowResponseModel
{
    [JsonProperty("rows")]
    public IEnumerable<GapResponseModel>? Gaps { get; set; }
}