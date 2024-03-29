namespace TravelMeNow.DataAccess.Entities;

public class Schedule: Entity
{
    public string Day { get; set; } = string.Empty;

    public string Opening { get; set; } = string.Empty;

    public string Closing { get; set; } = string.Empty;

    public Landmark Landmark { get; set; }

    public int LandmarkId { get; set; }

}
