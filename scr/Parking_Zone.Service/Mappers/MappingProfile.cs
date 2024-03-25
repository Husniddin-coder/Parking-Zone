using AutoMapper;
using Parking_Zone.Domain.Entities;

namespace Parking_Zone.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ParkingZone, ParkingZone>();
        CreateMap<Address, Address>();
    }
}
