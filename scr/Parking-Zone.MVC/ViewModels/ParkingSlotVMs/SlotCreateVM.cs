using Parking_Zone.Domain.Entities;
using Parking_Zone.Domain.Enums;

namespace Parking_Zone.MVC.ViewModels.ParkingSlotVMs;

public class SlotCreateVM
{
    public int Number { get; set; }

    public bool IsBooked { get; set; } = false;

    public decimal FeePerHour { get; set; }

    public SlotCategory Category { get; set; } = SlotCategory.Standart;

    public long ParkingZoneId { get; set; }

    public string ParkingZoneName { get; set; }

    public ParkingSlot MapToModel(ParkingZone existingZone)
    {
        return new()
        {
            Number = Number,
            IsBooked = IsBooked,
            FeePerHour = FeePerHour,
            Category = Category,
            ParkingZoneId = ParkingZoneId,
            ParkingZone = existingZone
        };
    }
}
