namespace TravelMeNow.Application.Exceptions;

public class AddressException: Exception
{
    public AddressException() { }

    public AddressException(string message) : base(message) { }

}
