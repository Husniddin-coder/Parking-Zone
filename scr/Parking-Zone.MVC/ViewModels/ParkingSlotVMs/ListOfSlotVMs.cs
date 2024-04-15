using Parking_Zone.Domain.Entities;
using Parking_Zone.Domain.Enums;

namespace Parking_Zone.MVC.ViewModels.ParkingSlotVMs;

public class ListOfSlotVMs
{
    public long Id { get; set; }

    public int Number { get; set; }

    public bool IsBooked { get; set; }

    public decimal FeePerHour { get; set; }

    public SlotCategory Category { get; set; }

    public long ParkingZoneId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public ListOfSlotVMs(ParkingSlot parkingSlot)
    {
        Id = parkingSlot.Id;
        Number = parkingSlot.Number;
        IsBooked = parkingSlot.IsBooked;
        FeePerHour = parkingSlot.FeePerHour;
        Category = parkingSlot.Category;
        ParkingZoneId = parkingSlot.ParkingZoneId;
        CreatedAt = parkingSlot.CreatedAt;
        UpdatedAt = parkingSlot.UpdateAt;
    }
}
