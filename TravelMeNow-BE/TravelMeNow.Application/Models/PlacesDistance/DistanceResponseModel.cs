﻿using Newtonsoft.Json;

namespace TravelMeNow.Application.Models.PlacesDistance;

[Serializable]
public class DistanceResponseModel
{
    [JsonProperty("elements")]
    public IEnumerable<InfoResponseModel>? Infos { get; set; }
}
