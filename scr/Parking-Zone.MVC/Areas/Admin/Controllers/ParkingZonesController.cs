using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking_Zone.Domain.Entities;
using Parking_Zone.MVC.ViewModels.ParkingZoneVMs;
using Parking_Zone.Service.Interfaces;
namespace Parking_Zone.MVC.Areas.Admin.Controllers;

[Authorize]
[Area("Admin")]
public class ParkingZonesController : Controller
{
    private readonly IParkingZoneService _parkingZoneService;
    private readonly IMapper _mapper;

    public ParkingZonesController(IParkingZoneService parkingZoneService, IMapper mapper)
    {
        _parkingZoneService = parkingZoneService;
        _mapper = mapper;
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
        var parkingZone = _parkingZoneService.RetrieveById(id);

        if (parkingZone == null)
            return NotFound();

        ParkingZoneDetailsVM resVM = new();

        return View(_mapper.Map(parkingZone, resVM));
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
            var parkingZone = new ParkingZone();
            _parkingZoneService.Insert(_mapper.Map(createVM, parkingZone));

            return RedirectToAction(nameof(Index));
        }
        return View(createVM);
    }

    // GET: Admin/ParkingZones/Edit/5
    public IActionResult Edit(long? id)
    {
        var parkingZone = _parkingZoneService.RetrieveById(id);

        if (parkingZone == null)
            return NotFound();

        ParkingZoneEditVM resVM = new();

        return View(_mapper.Map(parkingZone, resVM));
    }

    // POST: Admin/ParkingZones/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(long id, ParkingZoneEditVM parkingZoneEditVM)
    {
        if (id != parkingZoneEditVM.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                var parkingZone = _parkingZoneService.RetrieveById(id);
                _parkingZoneService.Modify(_mapper.Map(parkingZoneEditVM, parkingZone));
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
        if (parkingZone == null)
            return NotFound();

        var resVM = new ParkingZoneDeleteVM();

        return View(_mapper.Map(parkingZone, resVM));
    }

    // POST: Admin/ParkingZones/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(long id)
    {
        var existingParkingZone = _parkingZoneService.RetrieveById(id);
        if (existingParkingZone == null)
            return NotFound();

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
