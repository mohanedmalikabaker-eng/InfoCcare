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
    public class TaktikPostpaidsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaktikPostpaidsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TaktikPostpaids
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TaktikPostpaid.Include(t => t.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TaktikPostpaids/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taktikPostpaid = await _context.TaktikPostpaid
                .Include(t => t.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taktikPostpaid == null)
            {
                return NotFound();
            }

            return View(taktikPostpaid);
        }

        // GET: TaktikPostpaids/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: TaktikPostpaids/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaktikPostpaid taktikPostpaid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            taktikPostpaid.CreatedOn = DateTime.Now;
            taktikPostpaid.CreatedById = userId;
            _context.Add(taktikPostpaid);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
          
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", taktikPostpaid.CreatedById);
            return View(taktikPostpaid);
        }

        // GET: TaktikPostpaids/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taktikPostpaid = await _context.TaktikPostpaid.FindAsync(id);
            if (taktikPostpaid == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", taktikPostpaid.CreatedById);
            return View(taktikPostpaid);
        }

        // POST: TaktikPostpaids/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,TaktikPostpaid taktikPostpaid)
        {
            if (id != taktikPostpaid.Id)
            {
                return NotFound();
            }

           
                try
                {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                taktikPostpaid.CreatedOn = DateTime.Now;
                taktikPostpaid.CreatedById = userId;
                _context.Update(taktikPostpaid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaktikPostpaidExists(taktikPostpaid.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
         
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", taktikPostpaid.CreatedById);
            return View(taktikPostpaid);
        }

        // GET: TaktikPostpaids/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taktikPostpaid = await _context.TaktikPostpaid
                .Include(t => t.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taktikPostpaid == null)
            {
                return NotFound();
            }

            return View(taktikPostpaid);
        }

        // POST: TaktikPostpaids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taktikPostpaid = await _context.TaktikPostpaid.FindAsync(id);
            if (taktikPostpaid != null)
            {
                _context.TaktikPostpaid.Remove(taktikPostpaid);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaktikPostpaidExists(int id)
        {
            return _context.TaktikPostpaid.Any(e => e.Id == id);
        }
    }
}
