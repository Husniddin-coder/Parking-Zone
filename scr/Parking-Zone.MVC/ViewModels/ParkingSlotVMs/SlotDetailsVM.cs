using Parking_Zone.Domain.Entities;
using Parking_Zone.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.MVC.ViewModels.ParkingSlotVMs;

public class SlotDetailsVM
{
    [Required]
    public long Id { get; set; }

    [Required]
    public int Number { get; set; }

    [Required]
    public bool IsAvailable { get; set; } = false;

    [Required]
    public decimal FeePerHour { get; set; }

    [Required]
    public SlotCategory Category { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public long ParkingZoneId { get; set; }

    [Required]
    public string ParkingZoneName { get; set; }

    public SlotDetailsVM()
    { }

    public SlotDetailsVM(ParkingSlot parkingSlot)
    {
        Id = parkingSlot.Id;
        Number = parkingSlot.Number;
        IsAvailable = parkingSlot.IsAvailable;
        Category = parkingSlot.Category;
        FeePerHour = parkingSlot.FeePerHour;
        ParkingZoneName = parkingSlot.ParkingZone.Name;
        ParkingZoneId = parkingSlot.ParkingZoneId;
        CreatedAt = parkingSlot.CreatedAt;
    }
}
