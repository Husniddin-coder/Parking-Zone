using Parking_Zone.Domain.Entities;

namespace Parking_Zone.Service.Interfaces;

public interface IParkingZoneService
{
    Task<ParkingZone> RetrieveByIdAsync(long? id);

    Task<IEnumerable<ParkingZone>> RetrieveAllAsync();

    Task<ParkingZone> InsertAsync(ParkingZone parkingZone);

    Task<ParkingZone> ModifyAsync(long id,ParkingZone parkingZone);

    Task<bool> RemoveAsync(long id);
}
