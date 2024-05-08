using Parking_Zone.Domain.Entities;

namespace Parking_Zone.Service.Interfaces;

public interface IParkingSlotService : IService<ParkingSlot>
{
    IEnumerable<ParkingSlot> RetrieveByZoneId(long zoneId);

    bool SlotIsFoundWithThisNumber(int slotNumber, long zoneId);

    IEnumerable<ParkingSlot> GetFreeSlotsByZoneIdAndPeriod(long zoneId, DateTime startTime, int duration);

    bool FreeSlot(ParkingSlot parkingSlot, DateTime startTime, int duration);
}
