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
    public class TariffsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TariffsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tariffs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tariff.Include(t => t.CreatedBy).Include(t => t.Segment);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tariffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tariff = await _context.Tariff
                .Include(t => t.CreatedBy)
                .Include(t => t.Segment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tariff == null)
            {
                return NotFound();
            }

            return View(tariff);
        }

        // GET: Tariffs/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            ViewData["SegmentId"] = new SelectList(_context.Segments, "Id", "Title");
            return View();
        }

        // POST: Tariffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tariff tariff)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            tariff.CreatedOn = DateTime.Now;
            tariff.CreatedById = userId;
            _context.Add(tariff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", tariff.CreatedById);
            ViewData["SegmentId"] = new SelectList(_context.Segments, "Id", "Title", tariff.SegmentId);
            return View(tariff);
        }

        // GET: Tariffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tariff = await _context.Tariff.FindAsync(id);
            if (tariff == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", tariff.CreatedById);
            ViewData["SegmentId"] = new SelectList(_context.Segments, "Id", "Title", tariff.SegmentId);
            return View(tariff);
        }

        // POST: Tariffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Tariff tariff)
        {
            if (id != tariff.Id)
            {
                return NotFound();
            }

           
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    tariff.CreatedOn = DateTime.Now;
                    tariff.CreatedById = userId;
                    _context.Update(tariff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TariffExists(tariff.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
          
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", tariff.CreatedById);
            ViewData["SegmentId"] = new SelectList(_context.Segments, "Id", "Title", tariff.SegmentId);
            return View(tariff);
        }

        // GET: Tariffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tariff = await _context.Tariff
                .Include(t => t.CreatedBy)
                .Include(t => t.Segment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tariff == null)
            {
                return NotFound();
            }

            return View(tariff);
        }

        // POST: Tariffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tariff = await _context.Tariff.FindAsync(id);
            if (tariff != null)
            {
                _context.Tariff.Remove(tariff);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TariffExists(int id)
        {
            return _context.Tariff.Any(e => e.Id == id);
        }
    }
}
