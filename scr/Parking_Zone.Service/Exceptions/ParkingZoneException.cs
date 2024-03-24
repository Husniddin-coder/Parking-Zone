
namespace Parking_Zone.Service.Exceptions;

public class ParkingZoneException : System.Exception
{
    public int StatusCode { get; set; }

    public ParkingZoneException(int code, string message) : base(message)
    {
        StatusCode = code;
    }
}
