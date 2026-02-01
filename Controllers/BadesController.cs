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
    public class BadesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BadesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bades
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Bade.Include(b => b.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bade = await _context.Bade
                .Include(b => b.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bade == null)
            {
                return NotFound();
            }

            return View(bade);
        }

        // GET: Bades/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: Bades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bade bade)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bade.CreatedOn = DateTime.Now;
            bade.CreatedById = userId;
            _context.Add(bade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", bade.CreatedById);
            return View(bade);
        }

        // GET: Bades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bade = await _context.Bade.FindAsync(id);
            if (bade == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Id", bade.CreatedById);
            return View(bade);
        }

        // POST: Bades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Bade bade)
        {
            if (id != bade.Id)
            {
                return NotFound();
            }

          
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    bade.CreatedOn = DateTime.Now;
                    bade.CreatedById = userId;
                    _context.Update(bade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BadeExists(bade.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
           
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", bade.CreatedById);
            return View(bade);
        }

        // GET: Bades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bade = await _context.Bade
                .Include(b => b.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bade == null)
            {
                return NotFound();
            }

            return View(bade);
        }

        // POST: Bades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bade = await _context.Bade.FindAsync(id);
            if (bade != null)
            {
                _context.Bade.Remove(bade);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BadeExists(int id)
        {
            return _context.Bade.Any(e => e.Id == id);
        }
    }
}
