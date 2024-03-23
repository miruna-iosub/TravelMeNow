using Newtonsoft.Json;

namespace TravelMeNow.Application.Models.Address;

[Serializable]
public class ApiResultResponseModel
{
    [JsonProperty("results")]
    public IEnumerable<AddressResponseModel>? Addresses { get; set; }
}
