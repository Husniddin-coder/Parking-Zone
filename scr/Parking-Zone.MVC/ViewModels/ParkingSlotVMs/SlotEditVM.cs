using Parking_Zone.Domain.Entities;
using Parking_Zone.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.MVC.ViewModels.ParkingSlotVMs;

public class SlotEditVM
{
    [Required]
    public long Id { get; set; }

    [Required]
    public int Number { get; set; }

    [Required]
    public bool IsBooked { get; set; } = false;

    [Required]
    public decimal FeePerHour { get; set; }

    [Required]
    public SlotCategory Category { get; set; }

    [Required]
    public long ParkingZoneId { get; set; }

    [Required]
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
