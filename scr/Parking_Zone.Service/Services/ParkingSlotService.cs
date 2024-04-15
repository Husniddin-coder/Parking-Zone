using Parking_Zone.Data.IRepositories;
using Parking_Zone.Domain.Entities;
using Parking_Zone.Service.Interfaces;

namespace Parking_Zone.Service.Services;

public class ParkingSlotService : Service<ParkingSlot>, IParkingSlotService
{
    private readonly IParkingSlotRepository _slotRepository;

    public ParkingSlotService(
        IParkingSlotRepository repository)
        : base(repository)
    {
        _slotRepository = repository;
    }

    public override void Insert(ParkingSlot slot)
    {
        slot.UpdateAt = DateTime.Now;
        base.Insert(slot);
    }

    public IEnumerable<ParkingSlot> RetrieveByZoneId(long zoneId)
        => _slotRepository
        .GetAll()
        .Where(x=> x.ParkingZoneId == zoneId)
        .OrderBy(x=> x.Number);

    public bool SlotIsFoundWithThisNumber(int slotNumber,  long zoneId)
        => _slotRepository
        .GetAll()
        .Where(x=> x.ParkingZoneId == zoneId &&  x.Number == slotNumber)
        .Any();

}
