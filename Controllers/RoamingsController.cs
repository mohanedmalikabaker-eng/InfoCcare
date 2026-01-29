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
    public class RoamingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoamingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Roamings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Roaming.Include(r => r.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Roamings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roaming = await _context.Roaming
                .Include(r => r.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roaming == null)
            {
                return NotFound();
            }

            return View(roaming);
        }

        // GET: Roamings/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: Roamings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Roaming roaming)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            roaming.CreatedOn = DateTime.Now;
            roaming.CreatedById = userId;
            _context.Add(roaming);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
          
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", roaming.CreatedById);
            return View(roaming);
        }

        // GET: Roamings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roaming = await _context.Roaming.FindAsync(id);
            if (roaming == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", roaming.CreatedById);
            return View(roaming);
        }

        // POST: Roamings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Roaming roaming)
        {
            if (id != roaming.Id)
            {
                return NotFound();
            }

            
                try
                {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                roaming.CreatedOn = DateTime.Now;
                roaming.CreatedById = userId;
                _context.Update(roaming);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoamingExists(roaming.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
              
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", roaming.CreatedById);
            return View(roaming);
        }

        // GET: Roamings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roaming = await _context.Roaming
                .Include(r => r.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roaming == null)
            {
                return NotFound();
            }

            return View(roaming);
        }

        // POST: Roamings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roaming = await _context.Roaming.FindAsync(id);
            if (roaming != null)
            {
                _context.Roaming.Remove(roaming);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoamingExists(int id)
        {
            return _context.Roaming.Any(e => e.Id == id);
        }
    }
}
