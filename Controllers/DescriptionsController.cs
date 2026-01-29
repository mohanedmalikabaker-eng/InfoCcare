using InfoCcare.Data;
using InfoCcare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InfoCcare.Controllers
{
    public class DescriptionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DescriptionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Descriptions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Descriptions.Include(d => d.CreatedBy).Include(d => d.Segment);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Descriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var description = await _context.Descriptions
                .Include(d => d.CreatedBy)
                .Include(d => d.Segment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (description == null)
            {
                return NotFound();
            }

            return View(description);
        }

        // GET: Descriptions/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            ViewData["SegmentId"] = new SelectList(_context.Segments, "Id", "Title");
            return View();
        }

        // POST: Descriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Description description)
        {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                description.CreatedOn = DateTime.Now;
                description.CreatedById = userId;
                _context.Add(description);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
          
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", description.CreatedById);
            ViewData["SegmentId"] = new SelectList(_context.Segments, "Id", "Title", description.SegmentId);
            return View(description);
        }

        // GET: Descriptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var description = await _context.Descriptions.FindAsync(id);
            if (description == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", description.CreatedById);
            ViewData["SegmentId"] = new SelectList(_context.Segments, "Id", "Title", description.SegmentId);
            return View(description);
        }

        // POST: Descriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Description description)
        {
            if (id != description.Id)
            {
                return NotFound();
            }

            
                try
                {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                description.CreatedOn = DateTime.Now;
                description.CreatedById = userId;
                _context.Update(description);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DescriptionExists(description.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", description.CreatedById);
            ViewData["SegmentId"] = new SelectList(_context.Segments, "Id", "Title", description.SegmentId);
            return View(description);
        }

        // GET: Descriptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var description = await _context.Descriptions
                .Include(d => d.CreatedBy)
                .Include(d => d.Segment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (description == null)
            {
                return NotFound();
            }

            return View(description);
        }

        // POST: Descriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var description = await _context.Descriptions.FindAsync(id);
            if (description != null)
            {
                _context.Descriptions.Remove(description);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DescriptionExists(int id)
        {
            return _context.Descriptions.Any(e => e.Id == id);
        }
    }
}
