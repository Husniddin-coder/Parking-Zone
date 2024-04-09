using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking_Zone.MVC.ViewModels.ParkingZoneVMs;
using Parking_Zone.Service.Interfaces;
namespace Parking_Zone.MVC.Areas.Admin.Controllers;

[Authorize]
[Area("Admin")]
public class ParkingZonesController : Controller
{
    private readonly IParkingZoneService _parkingZoneService;
    private readonly IAddressService _addressService;

    public ParkingZonesController(IParkingZoneService parkingZoneService, IAddressService addressService)
    {
        _parkingZoneService = parkingZoneService;
        _addressService = addressService;
    }

    // GET: Admin/ParkingZones
    public IActionResult Index()
    {
        var parkingZones = _parkingZoneService.RetrieveAll();
        var VMs = parkingZones.Select(x=> new ListOfItemsVM(x));
        return View(VMs);
    }

    // GET: Admin/ParkingZones/Details/5
    public IActionResult Details(long? id)
    {
        var parkingZone = _parkingZoneService.RetrieveById(id);

        if (parkingZone is null)
            return NotFound();

        DetailsVM resVM = new DetailsVM(parkingZone);

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
    public IActionResult Create(CreateVM createVM)
    {
        if (ModelState.IsValid)
        {
            _parkingZoneService.Insert(createVM.MapToModel());

            return RedirectToAction(nameof(Index));
        }
        return View(createVM);
    }

    // GET: Admin/ParkingZones/Edit/5
    public IActionResult Edit(long? id)
    {
        var parkingZone = _parkingZoneService.RetrieveById(id);

        if (parkingZone is null)
            return NotFound();

        EditVM resVM = new(parkingZone);

        return View(resVM);
    }

    // POST: Admin/ParkingZones/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(long id, EditVM parkingZoneEditVM)
    {
        var existingParkingZone = _parkingZoneService.RetrieveById(id);

        if (id != existingParkingZone.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _parkingZoneService.Update(parkingZoneEditVM.MapToModel(existingParkingZone));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingZoneExists(parkingZoneEditVM.Id))
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
        var parkingZone = _parkingZoneService.RetrieveById(id);
        if (parkingZone is null)
            return NotFound();

        return View(parkingZone);
    }

    // POST: Admin/ParkingZones/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(long id)
    {
        var existingParkingZone = _parkingZoneService.RetrieveById(id);
        if (existingParkingZone is null)
            return NotFound();

        _parkingZoneService.Remove(id);
        _addressService.Remove(existingParkingZone.AddressId);

        return RedirectToAction(nameof(Index));
    }

    private bool ParkingZoneExists(long id)
    {
        var park = _parkingZoneService.RetrieveById(id);

        if (park is null)
            return false;

        return true;
    }
}
