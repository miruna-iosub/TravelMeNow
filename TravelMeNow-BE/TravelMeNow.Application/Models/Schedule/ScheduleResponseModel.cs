namespace TravelMeNow.Application.Models.Schedule;

public class ScheduleResponseModel
{
    public int Id { get; set; }

    public string Day { get; set; } = string.Empty;

    public string Opening { get; set; } = string.Empty;

    public string Closing { get; set; } = string.Empty;

    public int LandmarkId { get; set; }
}
