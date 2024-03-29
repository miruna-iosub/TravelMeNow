
namespace TravelMeNow.Application.Entities;

public record Point(double Lat, double Lng);

public record Geometry(Point Location);

public record Schedules(bool Open_Now);

public record Duration(string text);
