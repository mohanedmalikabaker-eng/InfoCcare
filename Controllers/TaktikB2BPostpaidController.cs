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
    public class TaktikB2BPostpaidController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaktikB2BPostpaidController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TaktikB2BPostpaid
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TaktikB2BPostpaid.Include(t => t.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TaktikB2BPostpaid/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taktikB2BPostpaid = await _context.TaktikB2BPostpaid
                .Include(t => t.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taktikB2BPostpaid == null)
            {
                return NotFound();
            }

            return View(taktikB2BPostpaid);
        }

        // GET: TaktikB2BPostpaid/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: TaktikB2BPostpaid/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( TaktikB2BPostpaid taktikB2BPostpaid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            taktikB2BPostpaid.CreatedOn = DateTime.Now;
            taktikB2BPostpaid.CreatedById = userId;
            _context.Add(taktikB2BPostpaid);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", taktikB2BPostpaid.CreatedById);
            return View(taktikB2BPostpaid);
        }

        // GET: TaktikB2BPostpaid/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taktikB2BPostpaid = await _context.TaktikB2BPostpaid.FindAsync(id);
            if (taktikB2BPostpaid == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", taktikB2BPostpaid.CreatedById);
            return View(taktikB2BPostpaid);
        }

        // POST: TaktikB2BPostpaid/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,TaktikB2BPostpaid taktikB2BPostpaid)
        {
            if (id != taktikB2BPostpaid.Id)
            {
                return NotFound();
            }

           
                try
                {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                taktikB2BPostpaid.CreatedOn = DateTime.Now;
                taktikB2BPostpaid.CreatedById = userId;
                _context.Update(taktikB2BPostpaid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaktikB2BPostpaidExists(taktikB2BPostpaid.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
            
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", taktikB2BPostpaid.CreatedById);
            return View(taktikB2BPostpaid);
        }

        // GET: TaktikB2BPostpaid/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taktikB2BPostpaid = await _context.TaktikB2BPostpaid
                .Include(t => t.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taktikB2BPostpaid == null)
            {
                return NotFound();
            }

            return View(taktikB2BPostpaid);
        }

        // POST: TaktikB2BPostpaid/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taktikB2BPostpaid = await _context.TaktikB2BPostpaid.FindAsync(id);
            if (taktikB2BPostpaid != null)
            {
                _context.TaktikB2BPostpaid.Remove(taktikB2BPostpaid);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaktikB2BPostpaidExists(int id)
        {
            return _context.TaktikB2BPostpaid.Any(e => e.Id == id);
        }
    }
}
