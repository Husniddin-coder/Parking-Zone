namespace Parking_Zone.Domain.Entities;

public class Reservation : Auditable
{
    public DateTime StartTime { get; set; }

    public int Duration { get; set; }

    public string VehiclePlateNumber { get; set; }

    public long ParkingSlotId { get; set; }

    public virtual ParkingSlot ParkingSlot { get; set; }

    public long UserId { get; set; }
}
