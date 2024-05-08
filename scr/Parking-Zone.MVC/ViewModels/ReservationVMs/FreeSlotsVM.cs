using Microsoft.AspNetCore.Mvc.Rendering;
using Parking_Zone.Domain.Entities;
using Parking_Zone.MVC.ViewModels.ParkingSlotVMs;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.MVC.ViewModels.ReservationVMs;

public class FreeSlotsVM
{

    public SelectList Zones { get; set; }

    [Required(ErrorMessage = "Please select a start time.")]
    public DateTime StartTime { get; set; }

    [Required(ErrorMessage = "Please enter a duration.")]
    [Range(1, int.MaxValue, ErrorMessage = "Duration must be greater than 0.")]
    public int Duration { get; set; }

    [Required(ErrorMessage = "Please select a zone.")]
    public long ZoneId { get; set; }

    public string ZoneName { get; set; }

    public IEnumerable<ListOfSlotVMs> FreeSlots { get; set; }

    public FreeSlotsVM()
    { }

    public FreeSlotsVM(IEnumerable<ParkingZone> zones)
    {
        Zones = new SelectList(zones, "Id", "Name");
    }
}
