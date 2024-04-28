using Microsoft.AspNetCore.Mvc;
using Parking_Zone.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Parking_Zone.MVC.ViewModels.ParkingSlotVMs;

namespace Parking_Zone.MVC.Areas.Admin.Controllers;

[Authorize]
[Area("Admin")]
public class ParkingSlotsController : Controller
{
    private readonly IParkingSlotService _slotService;
    private readonly IParkingZoneService _zoneService;

    public ParkingSlotsController(
        IParkingSlotService slotService,
        IParkingZoneService zoneService)
    {
        _slotService = slotService;
        _zoneService = zoneService;
    }

    public IActionResult Index(long id)
    {
        var slots = _slotService.RetrieveByZoneId(id);
        var zone = _zoneService.RetrieveById(id);

        var IndexVM = new IndexVM()
        {
            SlotsVMs = slots.Select(x => new ListOfSlotVMs(x)),
            ZoneName = zone.Name,
            ZoneId = id
        };

        return View(IndexVM);
    }

    public IActionResult Details(long id)
    {
        var slot = _slotService.RetrieveById(id);

        if (slot is null)
            return NotFound("Slot not found");

        var slotDetailsVM = new SlotDetailsVM(slot);
        return View(slotDetailsVM);
    }

    public IActionResult Create(long id)
    {
        var zone = _zoneService.RetrieveById(id);

        if (zone is null)
            return NotFound("Zone not found");
        
        var slotCreateVM = new SlotCreateVM();
        slotCreateVM.ParkingZoneId = id;
        slotCreateVM.ParkingZoneName = zone.Name;

        return View(slotCreateVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(SlotCreateVM slotCreateVM)
    {
        if (slotCreateVM.Number < 0)
            ModelState.AddModelError("Number", "Slot number cannot be negative");

        if (_slotService.SlotIsFoundWithThisNumber(slotCreateVM.Number, slotCreateVM.ParkingZoneId))
            ModelState.AddModelError("Number", "Slot with thus number already exists");

        if (ModelState.IsValid)
        {
            var existingZone = _zoneService.RetrieveById(slotCreateVM.ParkingZoneId);

            var slot = slotCreateVM.MapToModel(existingZone);
            _slotService.Insert(slot);
            return RedirectToAction("Index", "ParkingSlots", new { id = slotCreateVM.ParkingZoneId });
        }
        return View(slotCreateVM);
    }

    public IActionResult Edit(long id)
    {
        var slot = _slotService.RetrieveById(id);

        if (slot is null)
            return NotFound("Slot not found");

        var slotEditVM = new SlotEditVM(slot);
        return View(slotEditVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(long id, SlotEditVM slotEditVM)
    {
        if (slotEditVM.Number < 0)
            ModelState.AddModelError("Number", "Slot number cannot be negative");

        var existingSlot = _slotService.RetrieveById(id);

        if (existingSlot is null)
            return NotFound("Slot not found");

        if (existingSlot.Number != slotEditVM.Number)
            if (_slotService.SlotIsFoundWithThisNumber(slotEditVM.Number, slotEditVM.ParkingZoneId))
                ModelState.AddModelError("Number", "Slot with thus number already exists");

        if (ModelState.IsValid)
        {
            _slotService.Update(slotEditVM.MapToModel(existingSlot));

            return RedirectToAction(nameof(Index),
                "ParkingSlots", new { id = slotEditVM.ParkingZoneId });
        }
        return View(slotEditVM);
    }

    public IActionResult Delete(long? id)
    {
        var slot = _slotService.RetrieveById(id);

        if (slot is null)
            return NotFound("Slot not found");

        return View(slot);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(long id)
    {
        var existingSlot = _slotService.RetrieveById(id);

        if (existingSlot is null)
            return NotFound("Slot not found");

        _slotService.Remove(id);

        return RedirectToAction(nameof(Index),
               "ParkingSlots", new { id = existingSlot.ParkingZoneId });
    }
}
