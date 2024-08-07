﻿using Newtonsoft.Json;

namespace TravelMeNow.Application.Models.SpotsGap;

[Serializable]
public class GapResponseModel
{
    [JsonProperty("elements")]
    public IEnumerable<InfoResponseModel>? Details { get; set; }
}
