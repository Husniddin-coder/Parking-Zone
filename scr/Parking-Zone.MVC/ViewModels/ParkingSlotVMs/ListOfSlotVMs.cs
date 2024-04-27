using Parking_Zone.Domain.Entities;
using Parking_Zone.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.MVC.ViewModels.ParkingSlotVMs;

public class ListOfSlotVMs
{
    [Required]
    public long? Id { get; set; }

    [Required]
    public int Number { get; set; }

    [Required]
    public bool IsBooked { get; set; }

    [Required]
    public decimal FeePerHour { get; set; }

    [Required]
    public SlotCategory Category { get; set; }

    [Required]
    public long ParkingZoneId { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public ListOfSlotVMs()
    { }
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
