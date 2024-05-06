using Microsoft.AspNetCore.Mvc;
using Parking_Zone.Service.Interfaces;

namespace Parking_Zone.MVC.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IParkingZoneService _zoneService;

        public ReservationController(IParkingZoneService zoneService)
        {
            _zoneService = zoneService;
        }

        public IActionResult FreeSlots()
        {
            var zones = _zoneService.RetrieveAll();

            if (zones is null)
                return NotFound("Zones not found");

            return View(zones);
        }
    }
}
