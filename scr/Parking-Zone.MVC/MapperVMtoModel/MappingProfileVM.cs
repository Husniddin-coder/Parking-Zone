using AutoMapper;
using Parking_Zone.Domain.Entities;
using Parking_Zone.MVC.ViewModels.ParkingZoneVMs;

namespace Parking_Zone.MVC.MapperVMtoModel
{
    public class MappingProfileVM : Profile
    {
        public MappingProfileVM()
        {
            CreateMap<ParkingZoneEditVM, ParkingZone>()
                .ForMember(d => d.UpdateAt, op => op.MapFrom(x => DateTime.Now))
                .ForPath(d=> d.Address.Street, op=> op.MapFrom(x=> x.Street))
                .ForPath(d=> d.Address.City, op=> op.MapFrom(x=> x.City))
                .ForPath(d=> d.Address.Province, op=> op.MapFrom(x=> x.Province))
                .ForPath(d=> d.Address.Country, op=> op.MapFrom(x=> x.Country))
                .ForPath(d=> d.Address.PostalCode, op=> op.MapFrom(x=> x.PostalCode)).ReverseMap();

            CreateMap<ParkingZoneCreateVM, ParkingZone>()
                .ForMember(d=> d.Name, op=> op.MapFrom(x=> x.Name))
                .ForMember(d=> d.UpdateAt, op=> op.MapFrom(x=> DateTime.Now))
                .ForPath(d => d.Address.Street, op => op.MapFrom(x => x.Street))
                .ForPath(d => d.Address.City, op => op.MapFrom(x => x.City))
                .ForPath(d => d.Address.Province, op => op.MapFrom(x => x.Province))
                .ForPath(d => d.Address.Country, op => op.MapFrom(x => x.Country))
                .ForPath(d => d.Address.PostalCode, op => op.MapFrom(x => x.PostalCode)).ReverseMap();

            CreateMap<ParkingZoneDetailsVM, ParkingZone>()
                .ForPath(d => d.Address.Street, op => op.MapFrom(x => x.Street))
                .ForPath(d => d.Address.City, op => op.MapFrom(x => x.City))
                .ForPath(d => d.Address.Province, op => op.MapFrom(x => x.Province))
                .ForPath(d => d.Address.Country, op => op.MapFrom(x => x.Country))
                .ForPath(d => d.Address.PostalCode, op => op.MapFrom(x => x.PostalCode))
                .ForMember(d => d.UpdateAt, op => op.MapFrom(x => x.UpdatedAt))
                .ForMember(d => d.CreatedAt, op => op.MapFrom(x => x.CreatedAt)).ReverseMap();

            CreateMap<ParkingZoneDeleteVM, ParkingZone>()
                .ForPath(d => d.Address.Street, op => op.MapFrom(x => x.Street))
                .ForPath(d => d.Address.City, op => op.MapFrom(x => x.City))
                .ForPath(d => d.Address.Province, op => op.MapFrom(x => x.Province))
                .ForPath(d => d.Address.Country, op => op.MapFrom(x => x.Country))
                .ForPath(d => d.Address.PostalCode, op => op.MapFrom(x => x.PostalCode))
                .ForMember(d => d.UpdateAt, op => op.MapFrom(x => x.UpdatedAt))
                .ForMember(d => d.CreatedAt, op => op.MapFrom(x => x.CreatedAt)).ReverseMap();
        }
    }
}
