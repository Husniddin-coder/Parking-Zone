using Microsoft.AspNetCore.Mvc.Rendering;
using Parking_Zone.Domain.Entities;
using Parking_Zone.MVC.ViewModels.ParkingSlotVMs;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.MVC.ViewModels.ReservationVMs;

public class FreeSlotsVM
{
    public static DateTime date = DateTime.Now;

    [Required]
    public DateTime StartTime { get; set; } = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0);

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Duration must be greater than 0.")]
    public int Duration { get; set; }

    [Required]
    public long ZoneId { get; set; }

    public string ZoneName { get; set; }

    public SelectList Zones { get; set; }

    public IEnumerable<ListOfSlotVMs> FreeSlots { get; set; }

    public FreeSlotsVM()
    { }

    public FreeSlotsVM(IEnumerable<ParkingZone> zones)
    {
        Zones = new SelectList(zones, "Id", "Name");
    }
}
