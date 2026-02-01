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
    public class BadeFeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BadeFeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BadeFees
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BadeFees.Include(b => b.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BadeFees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var badeFees = await _context.BadeFees
                .Include(b => b.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (badeFees == null)
            {
                return NotFound();
            }

            return View(badeFees);
        }

        // GET: BadeFees/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: BadeFees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BadeFees badeFees)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            badeFees.CreatedOn = DateTime.Now;
            badeFees.CreatedById = userId;
            _context.Add(badeFees);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
       
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", badeFees.CreatedById);
            return View(badeFees);
        }

        // GET: BadeFees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var badeFees = await _context.BadeFees.FindAsync(id);
            if (badeFees == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", badeFees.CreatedById);
            return View(badeFees);
        }

        // POST: BadeFees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,BadeFees badeFees)
        {
            if (id != badeFees.Id)
            {
                return NotFound();
            }

            
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    badeFees.CreatedOn = DateTime.Now;
                    badeFees.CreatedById = userId;
                    _context.Update(badeFees);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BadeFeesExists(badeFees.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
           
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", badeFees.CreatedById);
            return View(badeFees);
        }

        // GET: BadeFees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var badeFees = await _context.BadeFees
                .Include(b => b.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (badeFees == null)
            {
                return NotFound();
            }

            return View(badeFees);
        }

        // POST: BadeFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var badeFees = await _context.BadeFees.FindAsync(id);
            if (badeFees != null)
            {
                _context.BadeFees.Remove(badeFees);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BadeFeesExists(int id)
        {
            return _context.BadeFees.Any(e => e.Id == id);
        }
    }
}
