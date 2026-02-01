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
    public class BaseTranLimitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BaseTranLimitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BaseTranLimits
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BaseTranLimits.Include(b => b.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BaseTranLimits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baseTranLimits = await _context.BaseTranLimits
                .Include(b => b.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (baseTranLimits == null)
            {
                return NotFound();
            }

            return View(baseTranLimits);
        }

        // GET: BaseTranLimits/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: BaseTranLimits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BaseTranLimits baseTranLimits)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            baseTranLimits.CreatedOn = DateTime.Now;
            baseTranLimits.CreatedById = userId;
            _context.Add(baseTranLimits);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
      
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", baseTranLimits.CreatedById);
            return View(baseTranLimits);
        }

        // GET: BaseTranLimits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baseTranLimits = await _context.BaseTranLimits.FindAsync(id);
            if (baseTranLimits == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", baseTranLimits.CreatedById);
            return View(baseTranLimits);
        }

        // POST: BaseTranLimits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,BaseTranLimits baseTranLimits)
        {
            if (id != baseTranLimits.Id)
            {
                return NotFound();
            }

            
                try
                {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                baseTranLimits.CreatedOn = DateTime.Now;
                baseTranLimits.CreatedById = userId;
                _context.Update(baseTranLimits);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaseTranLimitsExists(baseTranLimits.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
              
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", baseTranLimits.CreatedById);
            return View(baseTranLimits);
        }

        // GET: BaseTranLimits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baseTranLimits = await _context.BaseTranLimits
                .Include(b => b.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (baseTranLimits == null)
            {
                return NotFound();
            }

            return View(baseTranLimits);
        }

        // POST: BaseTranLimits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var baseTranLimits = await _context.BaseTranLimits.FindAsync(id);
            if (baseTranLimits != null)
            {
                _context.BaseTranLimits.Remove(baseTranLimits);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaseTranLimitsExists(int id)
        {
            return _context.BaseTranLimits.Any(e => e.Id == id);
        }
    }
}
