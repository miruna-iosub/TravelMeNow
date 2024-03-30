using TravelMeNow.Application.Entities;
using Newtonsoft.Json;

namespace TravelMeNow.Application.Models.Spot;

[Serializable]
public class SpotResponseModel
{
    public string? GoogleLocation { get; set; }

    public string? Name { get; set; }

    public Geometry? Geometry { get; set; }

    public double? Rating { get; set; }

    public Schedules? Schedules { get; set; }
    
    public string? SpotId { get; set; }
}
