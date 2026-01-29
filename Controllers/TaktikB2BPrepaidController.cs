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
    public class TaktikB2BPrepaidController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaktikB2BPrepaidController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TaktikB2BPrepaid
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TaktikB2BPrepaid.Include(t => t.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TaktikB2BPrepaid/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taktikB2BPrepaid = await _context.TaktikB2BPrepaid
                .Include(t => t.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taktikB2BPrepaid == null)
            {
                return NotFound();
            }

            return View(taktikB2BPrepaid);
        }

        // GET: TaktikB2BPrepaid/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: TaktikB2BPrepaid/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaktikB2BPrepaid taktikB2BPrepaid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            taktikB2BPrepaid.CreatedOn = DateTime.Now;
            taktikB2BPrepaid.CreatedById = userId;
            _context.Add(taktikB2BPrepaid);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", taktikB2BPrepaid.CreatedById);
            return View(taktikB2BPrepaid);
        }

        // GET: TaktikB2BPrepaid/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taktikB2BPrepaid = await _context.TaktikB2BPrepaid.FindAsync(id);
            if (taktikB2BPrepaid == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", taktikB2BPrepaid.CreatedById);
            return View(taktikB2BPrepaid);
        }

        // POST: TaktikB2BPrepaid/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,TaktikB2BPrepaid taktikB2BPrepaid)
        {
            if (id != taktikB2BPrepaid.Id)
            {
                return NotFound();
            }

            
                try
                {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                taktikB2BPrepaid.CreatedOn = DateTime.Now;
                taktikB2BPrepaid.CreatedById = userId;
                _context.Update(taktikB2BPrepaid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaktikB2BPrepaidExists(taktikB2BPrepaid.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
             
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Id", taktikB2BPrepaid.CreatedById);
            return View(taktikB2BPrepaid);
        }

        // GET: TaktikB2BPrepaid/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taktikB2BPrepaid = await _context.TaktikB2BPrepaid
                .Include(t => t.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taktikB2BPrepaid == null)
            {
                return NotFound();
            }

            return View(taktikB2BPrepaid);
        }

        // POST: TaktikB2BPrepaid/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taktikB2BPrepaid = await _context.TaktikB2BPrepaid.FindAsync(id);
            if (taktikB2BPrepaid != null)
            {
                _context.TaktikB2BPrepaid.Remove(taktikB2BPrepaid);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaktikB2BPrepaidExists(int id)
        {
            return _context.TaktikB2BPrepaid.Any(e => e.Id == id);
        }
    }
}
