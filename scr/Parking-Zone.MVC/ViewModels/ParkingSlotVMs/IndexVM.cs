using Parking_Zone.Domain.Entities;

namespace Parking_Zone.MVC.ViewModels.ParkingSlotVMs;

public class IndexVM
{
    public IEnumerable<ListOfSlotVMs> SlotsVMs { get; set; }

    public string ZoneName { get; set; }

    public long ZoneId { get; set; }
}
