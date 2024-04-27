using Parking_Zone.Domain.Entities;
using Parking_Zone.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.MVC.ViewModels.ParkingSlotVMs;

public class SlotCreateVM
{
    [Required]
    public int Number { get; set; }

    [Required]
    public bool IsAvailable { get; set; } = false;

    [Required]
    public decimal FeePerHour { get; set; }

    [Required]
    public SlotCategory Category { get; set; }

    [Required]
    public long ParkingZoneId { get; set; }

    [Required]
    public string ParkingZoneName { get; set; }

    public ParkingSlot MapToModel(ParkingZone existingZone)
    {
        return new()
        {
            Number = Number,
            IsAvailable = IsAvailable,
            FeePerHour = FeePerHour,
            Category = Category,
            ParkingZoneId = ParkingZoneId,
            ParkingZone = existingZone
        };
    }
}