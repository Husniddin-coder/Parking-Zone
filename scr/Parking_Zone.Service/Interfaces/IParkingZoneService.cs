using Parking_Zone.Domain.Entities;

namespace Parking_Zone.Service.Interfaces;

public interface IParkingZoneService
{
    ParkingZone RetrieveById(long? id);

    IEnumerable<ParkingZone> RetrieveAll();

    ParkingZone Insert(ParkingZone parkingZone);

    ParkingZone Modify(long id,ParkingZone parkingZone);

    bool Remove(long id);
}
