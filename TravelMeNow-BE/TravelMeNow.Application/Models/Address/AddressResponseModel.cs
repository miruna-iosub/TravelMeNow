using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TravelMeNow.Application.Models.Address;

[Serializable]
public class AddressResponseModel
{
    [JsonProperty("formatted_address")]
    public String? Address { get; set; }
}
