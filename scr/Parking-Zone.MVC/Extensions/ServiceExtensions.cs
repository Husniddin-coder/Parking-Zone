﻿using Parking_Zone.Data.IRepositories;
using Parking_Zone.Data.Repositories;
using Parking_Zone.Service.Interfaces;
using Parking_Zone.Service.Services;

namespace Parking_Zone.MVC.Extensions;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        //repositories
        services.AddScoped<IParkingZoneRepository, ParkingZoneRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IParkingSlotRepository, ParkingSlotRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();

        //services
        services.AddScoped<IParkingZoneService,ParkingZoneService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IParkingSlotService, ParkingSlotService>();
        services.AddScoped<IReservationService, ReservationService>();
    }
}
