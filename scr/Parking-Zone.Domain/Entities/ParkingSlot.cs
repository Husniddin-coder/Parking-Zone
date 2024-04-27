using Parking_Zone.Domain.Enums;

namespace Parking_Zone.Domain.Entities;

public class ParkingSlot : Auditable
{
    public int Number { get; set; }

    public bool IsAvailable { get; set; }

    public decimal FeePerHour { get; set; }

    public SlotCategory Category { get; set; }

    public long ParkingZoneId { get; set; }

    public virtual ParkingZone ParkingZone { get; set; }
}
