using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Parking_Zone.MVC.ViewModels.ParkingSlotVMs;
using Parking_Zone.MVC.ViewModels.ReservationVMs;
using Parking_Zone.Service.Interfaces;

namespace Parking_Zone.MVC.Controllers;

[Authorize]
public class ReservationController : Controller
{
    private readonly IParkingZoneService _zoneService;
    private readonly IParkingSlotService _slotService;

    public ReservationController(IParkingZoneService zoneService, IParkingSlotService slotService)
    {
        _zoneService = zoneService;
        _slotService = slotService;
    }

    public IActionResult FreeSlots()
    {
        var zones = _zoneService.RetrieveAll();

        if (zones is null)
            return NotFound("Zones not found");

        FreeSlotsVM freeSlots = new(zones);

        return View(freeSlots);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult FreeSlots(FreeSlotsVM freeSlotsVM)
    {
        var zones = _zoneService.RetrieveAll();
        freeSlotsVM.Zones = new SelectList(zones, "Id", "Name");

        if (!ModelState.IsValid)
            return View(freeSlotsVM);

        var zone = _zoneService.RetrieveById(freeSlotsVM.ZoneId);
        
        freeSlotsVM.ZoneName = zone.Name;
        freeSlotsVM.FreeSlots = _slotService
            .GetFreeSlotsByZoneIdAndPeriod(freeSlotsVM.ZoneId, freeSlotsVM.StartTime, freeSlotsVM.Duration)
            .Select(x => new ListOfSlotVMs(x));

        return View(freeSlotsVM);
    }
}
