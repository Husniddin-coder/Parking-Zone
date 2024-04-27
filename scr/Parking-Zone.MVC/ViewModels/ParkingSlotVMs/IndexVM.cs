using Parking_Zone.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.MVC.ViewModels.ParkingSlotVMs;

public class IndexVM
{
    [Required]
    public IEnumerable<ListOfSlotVMs> SlotsVMs { get; set; }

    [Required]
    public string ZoneName { get; set; }

    [Required]
    public long ZoneId { get; set; }
}
