namespace TravelMeNow.DataAccess.Entities;

public class PointOfInterest: Entity
{
    public String Name { get; set; } = string.Empty;

    public string Info { get; set; } = string.Empty;

    public IEnumerable<OpeningHour> OpeningHours { get; set; }

    public string Link { get; set; } = string.Empty;

}
