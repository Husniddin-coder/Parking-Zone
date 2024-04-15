using Parking_Zone.Domain.Entities;
using Parking_Zone.Domain.Enums;

namespace Parking_Zone.MVC.ViewModels.ParkingSlotVMs;

public class SlotEditVM
{
    public long Id { get; set; }

    public int Number { get; set; }

    public bool IsBooked { get; set; } = false;

    public decimal FeePerHour { get; set; }

    public SlotCategory Category { get; set; } = SlotCategory.Standart;

    public long ParkingZoneId { get; set; }

    public string ParkingZoneName { get; set; }

    public SlotEditVM()
    {}

    public SlotEditVM(ParkingSlot parkingSlot)
    {
        Id = parkingSlot.Id;
        Number = parkingSlot.Number;
        IsBooked = parkingSlot.IsBooked;
        Category = parkingSlot.Category;
        FeePerHour = parkingSlot.FeePerHour;
        ParkingZoneId = parkingSlot.ParkingZoneId;
        ParkingZoneName = parkingSlot.ParkingZone.Name;
    }

    public ParkingSlot MapToModel(ParkingSlot existingSlot)
    {
        existingSlot.Number = Number;
        existingSlot.IsBooked = IsBooked;
        existingSlot.FeePerHour = FeePerHour;
        existingSlot.Category = Category;
        existingSlot.ParkingZoneId = ParkingZoneId;
        existingSlot.UpdateAt = DateTime.Now;

        return existingSlot;
    }
}
