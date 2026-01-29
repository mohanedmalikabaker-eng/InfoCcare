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
    public class SegmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SegmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Segments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Segments.Include(s => s.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Segments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var segment = await _context.Segments
                .Include(s => s.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (segment == null)
            {
                return NotFound();
            }

            return View(segment);
        }

        // GET: Segments/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Segments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Segment segment)
        {
           
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                segment.CreatedOn = DateTime.Now;
                segment.CreatedById = userId;
                _context.Add(segment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
         
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", segment.CreatedById);
            return View(segment);
        }

        // GET: Segments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var segment = await _context.Segments.FindAsync(id);
            if (segment == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", segment.CreatedById);
            return View(segment);
        }

        // POST: Segments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Segment segment)
        {
            if (id != segment.Id)
            {
                return NotFound();
            }

                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    segment.CreatedOn = DateTime.Now;
                    segment.CreatedById = userId;
                    _context.Update(segment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SegmentExists(segment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
           
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", segment.CreatedById);
            return View(segment);
        }

        // GET: Segments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var segment = await _context.Segments
                .Include(s => s.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (segment == null)
            {
                return NotFound();
            }

            return View(segment);
        }

        // POST: Segments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var segment = await _context.Segments.FindAsync(id);
            if (segment != null)
            {
                _context.Segments.Remove(segment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SegmentExists(int id)
        {
            return _context.Segments.Any(e => e.Id == id);
        }
    }
}
