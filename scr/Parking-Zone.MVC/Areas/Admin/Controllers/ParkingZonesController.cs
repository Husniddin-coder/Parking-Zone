using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking_Zone.MVC.ViewModels;
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
    public IActionResult Index()
    {
        var parkingZones = _parkingZoneService.RetrieveAll();
        ParkingZoneIndexVM resVM = new()
        { ParkingZones = parkingZones };
        return View(resVM);
    }

    // GET: Admin/ParkingZones/Details/5
    public IActionResult Details(long? id)
    {
        if (id == null)
            return NotFound();

        var parkingZone = _parkingZoneService.RetrieveById(id);

        if (parkingZone == null)
            return NotFound();

        ParkingZoneDetailsVM resVM = new()
        { ParkingZone = parkingZone };

        return View(resVM);
    }

    // GET: Admin/ParkingZones/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Admin/ParkingZones/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ParkingZoneCreateVM createVM)
    {
        if (ModelState.IsValid)
        {
            _parkingZoneService.Insert(createVM.ParkingZone);

            return RedirectToAction(nameof(Index));
        }
        return View(createVM);
    }

    // GET: Admin/ParkingZones/Edit/5
    public IActionResult Edit(long? id)
    {
        if (id == null)
            return NotFound();

        var parkingZone = _parkingZoneService.RetrieveById(id);

        if (parkingZone == null)
            return NotFound();

        ParkingZoneEditVM resVM = new()
        { ParkingZone = parkingZone };

        return View(resVM);
    }

    // POST: Admin/ParkingZones/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(long id, ParkingZoneEditVM parkingZoneEditVM)
    {
        if (id != parkingZoneEditVM.ParkingZone.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _parkingZoneService.Modify(id, parkingZoneEditVM.ParkingZone);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingZoneExists(parkingZoneEditVM.ParkingZone.Id))
                    return NotFound();

                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(parkingZoneEditVM);
    }

    // GET: Admin/ParkingZones/Delete/5
    public IActionResult Delete(long? id)
    {
        if (id == null)
            return NotFound();

        var parkingZone = _parkingZoneService.RetrieveById(id);
        if (parkingZone == null)
            return NotFound();

        return View(parkingZone);
    }

    // POST: Admin/ParkingZones/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(long id)
    {
        _parkingZoneService.Remove(id);

        return RedirectToAction(nameof(Index));
    }

    private bool ParkingZoneExists(long id)
    {
        var park = _parkingZoneService.RetrieveById(id);

        if (park == null)
            return false;

        return true;
    }
}
