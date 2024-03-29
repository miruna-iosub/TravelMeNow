using TravelMeNow.Application.Entities;
using Newtonsoft.Json;

namespace TravelMeNow.Application.Models.Spot;

[Serializable]
public class SpotResponseModel
{
    [JsonProperty("vicinity")]
    public string? GoogleLocation { get; set; }

    public string? Name { get; set; }

    public Geometry? Geometry { get; set; }

    [JsonProperty("spot_id")]
    public string? SpotId { get; set; }

    public double? Rating { get; set; }

    public string? Icon { get; set; }

    [JsonProperty("opening_hours")]
    public Schedules? Schedules { get; set; }
}
