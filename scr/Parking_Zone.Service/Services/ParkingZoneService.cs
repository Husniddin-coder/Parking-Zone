
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

    public async Task<ParkingZone> InsertAsync(ParkingZone parkingZone)
    {
        var existPZ = await _parkingZoneRepository
            .GetAllAsync()
            .Where(x => x.Name == parkingZone.Name)
            .FirstOrDefaultAsync();

        if (existPZ != null)
            throw new ParkingZoneException(409, "Parking Zone already exists");
        parkingZone.UpdateAt = DateTime.Now;
        parkingZone.Address.UpdateAt = DateTime.Now;
        return await _parkingZoneRepository.CreateAsync(parkingZone);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var parkingZone = await _parkingZoneRepository
            .GetAsync(id);

        if (parkingZone != null)
            await _parkingZoneRepository.DeleteAsync(id);

        return await _addressRepository.DeleteAsync(parkingZone.AddressId);
    }

    public async Task<IEnumerable<ParkingZone>> RetrieveAllAsync()
    {
        return await _parkingZoneRepository
             .GetAllAsync()
             .Include(x => x.Address)
             .AsNoTracking()
             .ToListAsync();
    }

    public async Task<ParkingZone> RetrieveByIdAsync(long? id)
    {
        return await _parkingZoneRepository
            .GetAllAsync()
            .Where(x => x.Id == id)
            .Include(a => a.Address)
            .FirstOrDefaultAsync();
    }

    public async Task<ParkingZone> ModifyAsync(long id, ParkingZone parkingZone)
    {
        if (id != parkingZone.Id)
            throw new ParkingZoneException(404, "Parking Zone is not found");

        var existingZone = await _parkingZoneRepository
            .GetAllAsync()
            .Where(x => x.Id == id)
            .Include(a => a.Address)
            .FirstOrDefaultAsync();



        var NewPZ = _mapper.Map(parkingZone, existingZone);
        existingZone.AddressId = id;
        existingZone.UpdateAt = DateTime.Now;
        existingZone.Address.UpdateAt = DateTime.Now;
        await _parkingZoneRepository.UpdateAsync(existingZone);

        return NewPZ;
    }
}
