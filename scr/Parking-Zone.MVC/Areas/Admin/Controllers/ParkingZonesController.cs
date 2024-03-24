using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking_Zone.Domain.Entities;
using Parking_Zone.Service.Interfaces;
namespace Parking_Zone.MVC.Areas.Admin.Controllers;

[Authorize]
[Area("Admin")]
public class ParkingZonesController : Controller
{
    private readonly IParkingZoneService _parkingZoneService;

    public ParkingZonesController(IParkingZoneService parkingZoneService)
    {

        _parkingZoneService = parkingZoneService;
    }

    // GET: Admin/ParkingZones
    public async Task<IActionResult> Index()
    {
        return View(await _parkingZoneService.RetrieveAllAsync());
    }

    // GET: Admin/ParkingZones/Details/5
    public async Task<IActionResult> Details(long? id)
    {
        if (id == null)
            return NotFound();

        var parkingZone = await _parkingZoneService.RetrieveByIdAsync(id);

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
            await _parkingZoneService.InsertAsync(parkingZone);

            return RedirectToAction(nameof(Index));
        }
        return View(parkingZone);
    }

    // GET: Admin/ParkingZones/Edit/5
    public async Task<IActionResult> Edit(long? id)
    {
        if (id == null)
            return NotFound();

        var parkingZone = await _parkingZoneService.RetrieveByIdAsync(id);

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
                await _parkingZoneService.ModifyAsync(id, parkingZone);
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

        var parkingZone = await _parkingZoneService.RetrieveByIdAsync(id);
        if (parkingZone == null)
            return NotFound();

        return View(parkingZone);
    }

    // POST: Admin/ParkingZones/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        await _parkingZoneService.RemoveAsync(id);

        return RedirectToAction(nameof(Index));
    }

    private bool ParkingZoneExists(long id)
    {
        var park = _parkingZoneService.RetrieveByIdAsync(id);

        if (park == null)
            return false;

        return true;
    }
}
