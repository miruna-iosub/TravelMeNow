namespace TravelMeNow.Application.Models.Spot;

public class SpotRequestModel
{
    public double Lat { get; set; }

    public double Long { get; set; } 

    public string Query { get; set; } = string.Empty;
}
