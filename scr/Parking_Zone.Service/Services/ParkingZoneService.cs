
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Parking_Zone.Data.IRepositories;
using Parking_Zone.Domain.Entities;
using Parking_Zone.Service.Exceptions;
using Parking_Zone.Service.Interfaces;

namespace Parking_Zone.Service.Services;

public class ParkingZoneService : IParkingZoneService
{
    private readonly IRepository<ParkingZone> _parkingZoneRepository;
    private readonly IRepository<Address> _addressRepository;
    private readonly IMapper _mapper;

    public ParkingZoneService(IRepository<ParkingZone> parkingZoneRepository, IMapper mapper, IRepository<Address> addressRepository)
    {
        _parkingZoneRepository = parkingZoneRepository;
        _mapper = mapper;
        _addressRepository = addressRepository;
    }

    public ParkingZone Insert(ParkingZone parkingZone)
    {
        var existPZ =  _parkingZoneRepository
            .GetAll()
            .Where(x => x.Name == parkingZone.Name)
            .FirstOrDefault();

        if (existPZ != null)
            throw new ParkingZoneException(409, "Parking Zone already exists");
        parkingZone.UpdateAt = DateTime.Now;
        parkingZone.Address.UpdateAt = DateTime.Now;
        return _parkingZoneRepository.Create(parkingZone);
    }

    public bool Remove(long id)
    {
        var parkingZone = _parkingZoneRepository
            .Get(id);

        if (parkingZone != null)
             _parkingZoneRepository.Delete(id);

        return  _addressRepository.Delete(parkingZone.AddressId);
    }

    public IEnumerable<ParkingZone> RetrieveAll()
    {
        return _parkingZoneRepository
             .GetAll()
             .Include(x => x.Address)
             .AsNoTracking()
             .ToList();
    }

    public ParkingZone RetrieveById(long? id)
    {
        return  _parkingZoneRepository
            .GetAll()
            .Where(x => x.Id == id)
            .Include(a => a.Address)
            .FirstOrDefault();
    }

    public ParkingZone Modify(long id, ParkingZone parkingZone)
    {
        if (id != parkingZone.Id)
            throw new ParkingZoneException(404, "Parking Zone is not found");

        var existingZone = _parkingZoneRepository
            .GetAll()
            .Where(x => x.Id == id)
            .Include(a => a.Address)
            .FirstOrDefault();



        var NewPZ = _mapper.Map(parkingZone, existingZone);
        existingZone.UpdateAt = DateTime.Now;
        existingZone.Address.UpdateAt = DateTime.Now;
        _parkingZoneRepository.Update(existingZone);

        return NewPZ;
    }
}
