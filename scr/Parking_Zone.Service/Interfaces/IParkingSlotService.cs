using Parking_Zone.Domain.Entities;

namespace Parking_Zone.Service.Interfaces;

public interface IParkingSlotService : IService<ParkingSlot>
{
    IEnumerable<ParkingSlot> RetrieveByZoneId(long zoneId);
    bool SlotIsFoundWithThisNumber(int slotNumber, long zoneId);
}
