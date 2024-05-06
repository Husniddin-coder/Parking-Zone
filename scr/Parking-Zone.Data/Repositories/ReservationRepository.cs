using Parking_Zone.Data.DbContexts;
using Parking_Zone.Data.IRepositories;
using Parking_Zone.Domain.Entities;

namespace Parking_Zone.Data.Repositories;

public class ReservationRepository : Repository<Reservation> , IReservationRepository
{
    public ReservationRepository(ApplicationDbContext context) : base(context)
    { }
}