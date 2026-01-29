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
    public class TaktikPrepaidsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaktikPrepaidsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TaktikPrepaids
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TaktikPrepaids.Include(t => t.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TaktikPrepaids/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taktikPrepaid = await _context.TaktikPrepaids
                .Include(t => t.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taktikPrepaid == null)
            {
                return NotFound();
            }

            return View(taktikPrepaid);
        }

        // GET: TaktikPrepaids/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: TaktikPrepaids/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaktikPrepaid taktikPrepaid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            taktikPrepaid.CreatedOn = DateTime.Now;
            taktikPrepaid.CreatedById = userId;
            _context.Add(taktikPrepaid);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
         
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", taktikPrepaid.CreatedById);
            return View(taktikPrepaid);
        }

        // GET: TaktikPrepaids/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taktikPrepaid = await _context.TaktikPrepaids.FindAsync(id);
            if (taktikPrepaid == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", taktikPrepaid.CreatedById);
            return View(taktikPrepaid);
        }

        // POST: TaktikPrepaids/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,TaktikPrepaid taktikPrepaid)
        {
            if (id != taktikPrepaid.Id)
            {
                return NotFound();
            }

            
                try
                {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                taktikPrepaid.CreatedOn = DateTime.Now;
                taktikPrepaid.CreatedById = userId;
                _context.Update(taktikPrepaid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaktikPrepaidExists(taktikPrepaid.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
         
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", taktikPrepaid.CreatedById);
            return View(taktikPrepaid);
        }

        // GET: TaktikPrepaids/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taktikPrepaid = await _context.TaktikPrepaids
                .Include(t => t.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taktikPrepaid == null)
            {
                return NotFound();
            }

            return View(taktikPrepaid);
        }

        // POST: TaktikPrepaids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taktikPrepaid = await _context.TaktikPrepaids.FindAsync(id);
            if (taktikPrepaid != null)
            {
                _context.TaktikPrepaids.Remove(taktikPrepaid);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaktikPrepaidExists(int id)
        {
            return _context.TaktikPrepaids.Any(e => e.Id == id);
        }
    }
}
