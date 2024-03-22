using Parking_Zone.Data.IRepositories;
using Parking_Zone.Data.Repositories;

namespace Parking_Zone.MVC.Extensions;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IParkingZoneRepository, ParkingZoneRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
    }
}
