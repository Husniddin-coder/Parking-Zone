using Parking_Zone.Data.IRepositories;
using Parking_Zone.Domain.Entities;
using Parking_Zone.Service.Interfaces;

namespace Parking_Zone.Service.Services;

public class ReservationService : Service<Reservation> , IReservationService
{
    public ReservationService(IReservationRepository repository) 
        : base(repository)
    { }

    public override void Insert(Reservation entity)
    {
        entity.UpdateAt = DateTime.Now;
        base.Insert(entity);
    }
}
