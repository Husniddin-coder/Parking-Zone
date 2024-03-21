using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parking_Zone.MVC.Data;
using Parking_Zone.MVC.Models;

namespace Parking_Zone.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ParkingZonesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParkingZonesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ParkingZones
        public async Task<IActionResult> Index()
        {
            return View(await _context.ParkingZones.Include(x=>x.Address).ToListAsync());
        }

        // GET: Admin/ParkingZones/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingZone = await _context.ParkingZones.Include(x=> x.Address)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkingZone == null)
            {
                return NotFound();
            }

            return View(parkingZone);
        }

        // GET: Admin/ParkingZones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ParkingZones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ParkingZone parkingZone)
        {
            if (ModelState.IsValid)
            {
                parkingZone.UpdateAt = DateTime.Now;
                parkingZone.Address.UpdateAt = DateTime.Now;
                _context.Add(parkingZone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parkingZone);
        }

        // GET: Admin/ParkingZones/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingZone = await _context.ParkingZones.Where(x=> x.Id==id).Include(x=> x.Address).FirstOrDefaultAsync();
            if (parkingZone == null)
            {
                return NotFound();
            }
            return View(parkingZone);
        }

        // POST: Admin/ParkingZones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, ParkingZone parkingZone)
        {
            if (id != parkingZone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingZone = await _context.ParkingZones.Where(x => x.Id == id)
                        .Include(x=> x.Address).FirstOrDefaultAsync();

                        existingZone.Name = parkingZone.Name;

                        existingZone.Address.Street = parkingZone.Address.Street;
                        existingZone.Address.City = parkingZone.Address.City;
                        existingZone.Address.Country = parkingZone.Address.Country;
                        existingZone.Address.PostalCode = parkingZone.Address.PostalCode;
                        existingZone.Address.Province = parkingZone.Address.Province;

                        existingZone.UpdateAt = DateTime.Now;
                        existingZone.Address.UpdateAt = DateTime.Now;
                    _context.ParkingZones.Update(existingZone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkingZoneExists(parkingZone.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(parkingZone);
        }

        // GET: Admin/ParkingZones/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingZone = await _context.ParkingZones.Where(x=> x.Id == id).Include(x => x.Address)
                .FirstOrDefaultAsync();
            if (parkingZone == null)
            {
                return NotFound();
            }

            return View(parkingZone);
        }

        // POST: Admin/ParkingZones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var parkingZone = await _context.ParkingZones.FindAsync(id);
            if (parkingZone != null)
            {
                _context.ParkingZones.Remove(parkingZone);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkingZoneExists(long id)
        {
            return _context.ParkingZones.Any(e => e.Id == id);
        }
    }
}
