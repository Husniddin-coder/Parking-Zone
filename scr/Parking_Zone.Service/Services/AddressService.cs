using Parking_Zone.Data.IRepositories;
using Parking_Zone.Domain.Entities;
using Parking_Zone.Service.Interfaces;

namespace Parking_Zone.Service.Services;

public class AddressService : Service<Address>, IAddressService
{
    public AddressService(IAddressRepository repository)
        : base(repository)
    { }

    public override void Insert(Address entity)
    {
        entity.UpdateAt = DateTime.Now;
        base.Insert(entity);
    }
}
