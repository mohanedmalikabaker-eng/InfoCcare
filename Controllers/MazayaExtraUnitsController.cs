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
    public class MazayaExtraUnitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MazayaExtraUnitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MazayaExtraUnits
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MazayaExtraUnits.Include(m => m.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MazayaExtraUnits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mazayaExtraUnits = await _context.MazayaExtraUnits
                .Include(m => m.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mazayaExtraUnits == null)
            {
                return NotFound();
            }

            return View(mazayaExtraUnits);
        }

        // GET: MazayaExtraUnits/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: MazayaExtraUnits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MazayaExtraUnits mazayaExtraUnits)
        {
           
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            mazayaExtraUnits.CreatedOn = DateTime.Now;
            mazayaExtraUnits.CreatedById = userId;
                _context.Add(mazayaExtraUnits);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", mazayaExtraUnits.CreatedById);
            return View(mazayaExtraUnits);
        }

        // GET: MazayaExtraUnits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mazayaExtraUnits = await _context.MazayaExtraUnits.FindAsync(id);
            if (mazayaExtraUnits == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Id", mazayaExtraUnits.CreatedById);
            return View(mazayaExtraUnits);
        }

        // POST: MazayaExtraUnits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MazayaExtraUnits mazayaExtraUnits)
        {
            if (id != mazayaExtraUnits.Id)
            {
                return NotFound();
            }

            
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    mazayaExtraUnits.CreatedOn = DateTime.Now;
                    mazayaExtraUnits.CreatedById = userId;
                    _context.Update(mazayaExtraUnits);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MazayaExtraUnitsExists(mazayaExtraUnits.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
      
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", mazayaExtraUnits.CreatedById);
            return View(mazayaExtraUnits);
        }

        // GET: MazayaExtraUnits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mazayaExtraUnits = await _context.MazayaExtraUnits
                .Include(m => m.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mazayaExtraUnits == null)
            {
                return NotFound();
            }

            return View(mazayaExtraUnits);
        }

        // POST: MazayaExtraUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mazayaExtraUnits = await _context.MazayaExtraUnits.FindAsync(id);
            if (mazayaExtraUnits != null)
            {
                _context.MazayaExtraUnits.Remove(mazayaExtraUnits);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MazayaExtraUnitsExists(int id)
        {
            return _context.MazayaExtraUnits.Any(e => e.Id == id);
        }
    }
}
