namespace TravelMeNow.DataAccess.Entities;

public class Landmark: Entity
{
    public String Name { get; set; } = string.Empty;

    public string Info { get; set; } = string.Empty;

    public IEnumerable<Schedule> Schedules { get; set; }

    public string Link { get; set; } = string.Empty;

}
