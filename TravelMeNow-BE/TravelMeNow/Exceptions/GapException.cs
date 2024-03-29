namespace TravelMeNow.Application.Exceptions;

public class GapException: Exception
{
    public GapException() { }

    public GapException(string message) : base(message) { }
}
