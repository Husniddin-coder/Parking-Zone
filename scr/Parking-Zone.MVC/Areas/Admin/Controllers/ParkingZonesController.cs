using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking_Zone.Data.IRepositories;
using Parking_Zone.Domain.Entities;
namespace Parking_Zone.MVC.Areas.Admin.Controllers;

[Authorize]
[Area("Admin")]
public class ParkingZonesController : Controller
{
    private readonly IRepository<ParkingZone> _parkingZoneRepository;
    private readonly IRepository<Address> _addressRepository; // I need to delete address from database

    public ParkingZonesController(IRepository<ParkingZone> parkingZoneRepository, IRepository<Address> addressRepository)
    {
        _parkingZoneRepository = parkingZoneRepository;
        _addressRepository = addressRepository;
    }

    // GET: Admin/ParkingZones
    public async Task<IActionResult> Index()
    {
        return View(await _parkingZoneRepository
            .GetAllAsync()
            .Include(x => x.Address)
            .ToListAsync());
    }

    // GET: Admin/ParkingZones/Details/5
    public async Task<IActionResult> Details(long? id)
    {
        if (id == null)
            return NotFound();

        var parkingZone = await _parkingZoneRepository
            .GetAllAsync()
            .Where(x => x.Id == id)
            .Include(x => x.Address)
            .FirstOrDefaultAsync();

        if (parkingZone == null)
            return NotFound();

        return View(parkingZone);
    }

    // GET: Admin/ParkingZones/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Admin/ParkingZones/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ParkingZone parkingZone)
    {
        if (ModelState.IsValid)
        {
            parkingZone.UpdateAt = DateTime.Now;
            parkingZone.Address.UpdateAt = DateTime.Now;
            await _parkingZoneRepository.CreateAsync(parkingZone);

            return RedirectToAction(nameof(Index));
        }
        return View(parkingZone);
    }

    // GET: Admin/ParkingZones/Edit/5
    public async Task<IActionResult> Edit(long? id)
    {
        if (id == null)
            return NotFound();

        var parkingZone = await _parkingZoneRepository
            .GetAllAsync()
            .Where(x => x.Id == id)
            .Include(x => x.Address)
            .FirstOrDefaultAsync();

        if (parkingZone == null)
            return NotFound();

        return View(parkingZone);
    }

    // POST: Admin/ParkingZones/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long id, ParkingZone parkingZone)
    {
        if (id != parkingZone.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                var existingZone = await _parkingZoneRepository
                    .GetAllAsync()
                    .Where(x => x.Id == id)
                    .Include(x => x.Address)
                    .FirstOrDefaultAsync();

                existingZone.Name = parkingZone.Name;

                existingZone.Address.Street = parkingZone.Address.Street;
                existingZone.Address.City = parkingZone.Address.City;
                existingZone.Address.Country = parkingZone.Address.Country;
                existingZone.Address.PostalCode = parkingZone.Address.PostalCode;
                existingZone.Address.Province = parkingZone.Address.Province;

                existingZone.UpdateAt = DateTime.Now;
                existingZone.Address.UpdateAt = DateTime.Now;
                await _parkingZoneRepository.UpdateAsync(existingZone);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingZoneExists(parkingZone.Id))
                    return NotFound();

                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(parkingZone);
    }

    // GET: Admin/ParkingZones/Delete/5
    public async Task<IActionResult> Delete(long? id)
    {
        if (id == null)
            return NotFound();


        var parkingZone = await _parkingZoneRepository
            .GetAllAsync()
            .Where(x => x.Id == id)
            .Include(x => x.Address)
            .FirstOrDefaultAsync();
        if (parkingZone == null)
            return NotFound();

        return View(parkingZone);
    }

    // POST: Admin/ParkingZones/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        var parkingZone = await _parkingZoneRepository
            .GetAsync(id);

        if (parkingZone != null)
            await _parkingZoneRepository.DeleteAsync(id);

        await _addressRepository.DeleteAsync(parkingZone.AddressId);

        return RedirectToAction(nameof(Index));
    }

    private bool ParkingZoneExists(long id)
    {
        return _parkingZoneRepository.GetAllAsync().Any(e => e.Id == id);
    }
}
