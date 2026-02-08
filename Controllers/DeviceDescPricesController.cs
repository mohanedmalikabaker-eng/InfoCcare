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
    public class DeviceDescPricesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeviceDescPricesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DeviceDescPrices
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DeviceDescPrice.Include(d => d.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DeviceDescPrices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceDescPrice = await _context.DeviceDescPrice
                .Include(d => d.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deviceDescPrice == null)
            {
                return NotFound();
            }

            return View(deviceDescPrice);
        }

        // GET: DeviceDescPrices/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: DeviceDescPrices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeviceDescPrice deviceDescPrice)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            deviceDescPrice.CreatedOn = DateTime.Now;
            deviceDescPrice.CreatedById = userId;
            _context.Add(deviceDescPrice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
          
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", deviceDescPrice.CreatedById);
            return View(deviceDescPrice);
        }

        // GET: DeviceDescPrices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceDescPrice = await _context.DeviceDescPrice.FindAsync(id);
            if (deviceDescPrice == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Id", deviceDescPrice.CreatedById);
            return View(deviceDescPrice);
        }

        // POST: DeviceDescPrices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,DeviceDescPrice deviceDescPrice)
        {
            if (id != deviceDescPrice.Id)
            {
                return NotFound();
            }

            
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    deviceDescPrice.CreatedOn = DateTime.Now;
                    deviceDescPrice.CreatedById = userId;
                    _context.Update(deviceDescPrice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceDescPriceExists(deviceDescPrice.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
           
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", deviceDescPrice.CreatedById);
            return View(deviceDescPrice);
        }

        // GET: DeviceDescPrices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceDescPrice = await _context.DeviceDescPrice
                .Include(d => d.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deviceDescPrice == null)
            {
                return NotFound();
            }

            return View(deviceDescPrice);
        }

        // POST: DeviceDescPrices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deviceDescPrice = await _context.DeviceDescPrice.FindAsync(id);
            if (deviceDescPrice != null)
            {
                _context.DeviceDescPrice.Remove(deviceDescPrice);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceDescPriceExists(int id)
        {
            return _context.DeviceDescPrice.Any(e => e.Id == id);
        }
    }
}
