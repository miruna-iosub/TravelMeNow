﻿using TravelMeNow.Application.Entities;
using Newtonsoft.Json;

namespace TravelMeNow.Application.Models.Place;

[Serializable]
public class PlaceResponseModel
{
    [JsonProperty("vicinity")]
    public string? Address { get; set; }

    public string? Name { get; set; }

    public Geometry? Geometry { get; set; }

    [JsonProperty("place_id")]
    public string? PlaceId { get; set; }

    public double? Rating { get; set; }

    public string? Icon { get; set; }

    [JsonProperty("opening_hours")]
    public Schedules? Schedules { get; set; }
}
