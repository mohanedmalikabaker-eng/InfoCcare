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
    public class TarifffsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TarifffsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tarifffs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tarifff.Include(t => t.CreatedBy).Include(t => t.Segment);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tarifffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarifff = await _context.Tarifff
                .Include(t => t.CreatedBy)
                .Include(t => t.Segment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarifff == null)
            {
                return NotFound();
            }

            return View(tarifff);
        }

        // GET: Tarifffs/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            ViewData["SegmentId"] = new SelectList(_context.Segments, "Id", "Title");
            return View();
        }

        // POST: Tarifffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tarifff tarifff)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            tarifff.CreatedOn = DateTime.Now;
            tarifff.CreatedById = userId;
            _context.Add(tarifff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
          
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", tarifff.CreatedById);
            ViewData["SegmentId"] = new SelectList(_context.Segments, "Id", "Title", tarifff.SegmentId);
            return View(tarifff);
        }

        // GET: Tarifffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarifff = await _context.Tarifff.FindAsync(id);
            if (tarifff == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", tarifff.CreatedById);
            ViewData["SegmentId"] = new SelectList(_context.Segments, "Id", "Title", tarifff.SegmentId);
            return View(tarifff);
        }

        // POST: Tarifffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Tarifff tarifff)
        {
            if (id != tarifff.Id)
            {
                return NotFound();
            }

           
                try
                {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                tarifff.CreatedOn = DateTime.Now;
                tarifff.CreatedById = userId;
                _context.Update(tarifff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarifffExists(tarifff.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
           
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", tarifff.CreatedById);
            ViewData["SegmentId"] = new SelectList(_context.Segments, "Id", "Title", tarifff.SegmentId);
            return View(tarifff);
        }

        // GET: Tarifffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarifff = await _context.Tarifff
                .Include(t => t.CreatedBy)
                .Include(t => t.Segment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarifff == null)
            {
                return NotFound();
            }

            return View(tarifff);
        }

        // POST: Tarifffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarifff = await _context.Tarifff.FindAsync(id);
            if (tarifff != null)
            {
                _context.Tarifff.Remove(tarifff);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarifffExists(int id)
        {
            return _context.Tarifff.Any(e => e.Id == id);
        }
    }
}
