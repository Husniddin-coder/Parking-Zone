using Parking_Zone.Data.IRepositories;
using Parking_Zone.Domain.Entities;

namespace Parking_Zone.Data.Repositories;

public class ParkingZoneRepository : Repository<ParkingZone> , IParkingZoneRepository
{
    public ParkingZoneRepository(ApplicationDbContext context) : base(context)
    { }
}
