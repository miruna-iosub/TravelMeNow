namespace TravelMeNow.Application.Models.Schedule;

public class ScheduleRequestModel
{
    public string Day { get; set; }

    public string Opening { get; set; }

    public string Closing { get; set; }

    public int LandmarkId { get; set; }
}
