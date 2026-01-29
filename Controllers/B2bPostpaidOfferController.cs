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
    public class B2bPostpaidOfferController : Controller
    {
        private readonly ApplicationDbContext _context;

        public B2bPostpaidOfferController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: B2bPostpaidOffer
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.B2bPostpaidOffer.Include(b => b.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: B2bPostpaidOffer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var b2bPostpaidOffer = await _context.B2bPostpaidOffer
                .Include(b => b.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (b2bPostpaidOffer == null)
            {
                return NotFound();
            }

            return View(b2bPostpaidOffer);
        }

        // GET: B2bPostpaidOffer/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: B2bPostpaidOffer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( B2bPostpaidOffer b2bPostpaidOffer)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            b2bPostpaidOffer.CreatedOn = DateTime.Now;
            b2bPostpaidOffer.CreatedById = userId;
            _context.Add(b2bPostpaidOffer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", b2bPostpaidOffer.CreatedById);
            return View(b2bPostpaidOffer);
        }

        // GET: B2bPostpaidOffer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var b2bPostpaidOffer = await _context.B2bPostpaidOffer.FindAsync(id);
            if (b2bPostpaidOffer == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", b2bPostpaidOffer.CreatedById);
            return View(b2bPostpaidOffer);
        }

        // POST: B2bPostpaidOffer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, B2bPostpaidOffer b2bPostpaidOffer)
        {
            if (id != b2bPostpaidOffer.Id)
            {
                return NotFound();
            }

            
                try
                {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                b2bPostpaidOffer.CreatedOn = DateTime.Now;
                b2bPostpaidOffer.CreatedById = userId;
                _context.Update(b2bPostpaidOffer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!B2bPostpaidOfferExists(b2bPostpaidOffer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
        
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", b2bPostpaidOffer.CreatedById);
            return View(b2bPostpaidOffer);
        }

        // GET: B2bPostpaidOffer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var b2bPostpaidOffer = await _context.B2bPostpaidOffer
                .Include(b => b.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (b2bPostpaidOffer == null)
            {
                return NotFound();
            }

            return View(b2bPostpaidOffer);
        }

        // POST: B2bPostpaidOffer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var b2bPostpaidOffer = await _context.B2bPostpaidOffer.FindAsync(id);
            if (b2bPostpaidOffer != null)
            {
                _context.B2bPostpaidOffer.Remove(b2bPostpaidOffer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool B2bPostpaidOfferExists(int id)
        {
            return _context.B2bPostpaidOffer.Any(e => e.Id == id);
        }
    }
}
