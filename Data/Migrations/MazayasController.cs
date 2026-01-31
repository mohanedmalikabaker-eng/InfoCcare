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

namespace InfoCcare.Data.Migrations
{
    public class MazayasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MazayasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Mazayas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Mazaya.Include(m => m.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Mazayas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mazaya = await _context.Mazaya
                .Include(m => m.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mazaya == null)
            {
                return NotFound();
            }

            return View(mazaya);
        }

        // GET: Mazayas/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: Mazayas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Mazaya mazaya)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            mazaya.CreatedOn = DateTime.Now;
            mazaya.CreatedById = userId;
            _context.Add(mazaya);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
         
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", mazaya.CreatedById);
            return View(mazaya);
        }

        // GET: Mazayas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mazaya = await _context.Mazaya.FindAsync(id);
            if (mazaya == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", mazaya.CreatedById);
            return View(mazaya);
        }

        // POST: Mazayas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Mazaya mazaya)
        {
            if (id != mazaya.Id)
            {
                return NotFound();
            }

            
                try
                {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                mazaya.CreatedOn = DateTime.Now;
                mazaya.CreatedById = userId;
                _context.Update(mazaya);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MazayaExists(mazaya.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
         
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", mazaya.CreatedById);
            return View(mazaya);
        }

        // GET: Mazayas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mazaya = await _context.Mazaya
                .Include(m => m.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mazaya == null)
            {
                return NotFound();
            }

            return View(mazaya);
        }

        // POST: Mazayas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mazaya = await _context.Mazaya.FindAsync(id);
            if (mazaya != null)
            {
                _context.Mazaya.Remove(mazaya);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MazayaExists(int id)
        {
            return _context.Mazaya.Any(e => e.Id == id);
        }
    }
}
