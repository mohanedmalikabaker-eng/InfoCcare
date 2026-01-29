using InfoCcare.Data;
using InfoCcare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InfoCcare.Controllers
{
    public class ZonesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZonesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Zones
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Zones.Include(z => z.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Zones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = await _context.Zones
                .Include(z => z.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }

        // GET: Zones/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: Zones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Zone zone)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            zone.CreatedOn = DateTime.Now;
            zone.CreatedById = userId;
            _context.Add(zone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", zone.CreatedById);
            return View(zone);
        }

        // GET: Zones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = await _context.Zones.FindAsync(id);
            if (zone == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", zone.CreatedById);
            return View(zone);
        }

        // POST: Zones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Zone zone)
        {
            if (id != zone.Id)
            {
                return NotFound();
            }

            
                try
                {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                zone.CreatedOn = DateTime.Now;
                zone.CreatedById = userId;
                _context.Update(zone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZoneExists(zone.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
         
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", zone.CreatedById);
            return View(zone);
        }

        // GET: Zones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = await _context.Zones
                .Include(z => z.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }

        // POST: Zones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zone = await _context.Zones.FindAsync(id);
            if (zone != null)
            {
                _context.Zones.Remove(zone);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZoneExists(int id)
        {
            return _context.Zones.Any(e => e.Id == id);
        }
    }
}
