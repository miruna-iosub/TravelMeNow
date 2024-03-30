using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TravelMeNow.Application.Models.GoogleLocation;

[Serializable]
public class GoogleLocationResponseModel
{
    public String? GoogleLocation { get; set; }
}
