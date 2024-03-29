using Parking_Zone.Data.IRepositories;
using Parking_Zone.Data.Repositories;
using Parking_Zone.Service.Interfaces;
using Parking_Zone.Service.Services;

namespace Parking_Zone.MVC.Extensions;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IParkingZoneRepository, ParkingZoneRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();

        //services
        services.AddScoped(typeof(IService<>), typeof(Service<>));
        services.AddScoped<IParkingZoneService,ParkingZoneService>();
    }
}
