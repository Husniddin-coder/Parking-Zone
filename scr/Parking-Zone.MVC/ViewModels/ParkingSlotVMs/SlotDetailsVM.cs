using Parking_Zone.Domain.Entities;
using Parking_Zone.Domain.Enums;

namespace Parking_Zone.MVC.ViewModels.ParkingSlotVMs;

public class SlotDetailsVM
{
    public long Id { get; set; }

    public int Number { get; set; }

    public bool IsBooked { get; set; } = false;

    public decimal FeePerHour { get; set; }

    public SlotCategory Category { get; set; } = SlotCategory.Standart;

    public DateTime CreatedAt { get; set; }

    public long ParkingZoneId { get; set; }

    public string ParkingZoneName { get; set; }

    public SlotDetailsVM()
    { }

    public SlotDetailsVM(ParkingSlot parkingSlot)
    {
        Id = parkingSlot.Id;
        Number = parkingSlot.Number;
        IsBooked = parkingSlot.IsBooked;
        Category = parkingSlot.Category;
        FeePerHour = parkingSlot.FeePerHour;
        ParkingZoneName = parkingSlot.ParkingZone.Name;
        ParkingZoneId = parkingSlot.ParkingZoneId;
        CreatedAt = parkingSlot.CreatedAt;
    }
}
