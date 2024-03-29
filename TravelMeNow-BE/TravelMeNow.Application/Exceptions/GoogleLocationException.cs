namespace TravelMeNow.Application.Exceptions;

public class GoogleLocationException: Exception
{
    public GoogleLocationException() { }

    public GoogleLocationException(string message) : base(message) { }

}
