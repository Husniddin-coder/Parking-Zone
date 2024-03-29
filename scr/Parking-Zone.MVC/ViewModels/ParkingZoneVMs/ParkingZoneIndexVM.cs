using Parking_Zone.Domain.Entities;

namespace Parking_Zone.MVC.ViewModels.ParkingZoneVMs;

public class ParkingZoneIndexVM
{
    public IEnumerable<ParkingZone> ParkingZones { get; set; }
}
