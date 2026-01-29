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
    public class B2bPrepaidOfferController : Controller
    {
        private readonly ApplicationDbContext _context;

        public B2bPrepaidOfferController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: B2bPrepaidOffer
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.B2bPrepaidOffer.Include(b => b.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: B2bPrepaidOffer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var b2bPrepaidOffer = await _context.B2bPrepaidOffer
                .Include(b => b.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (b2bPrepaidOffer == null)
            {
                return NotFound();
            }

            return View(b2bPrepaidOffer);
        }

        // GET: B2bPrepaidOffer/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: B2bPrepaidOffer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(B2bPrepaidOffer b2bPrepaidOffer)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            b2bPrepaidOffer.CreatedOn = DateTime.Now;
            b2bPrepaidOffer.CreatedById = userId;
            _context.Add(b2bPrepaidOffer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", b2bPrepaidOffer.CreatedById);
            return View(b2bPrepaidOffer);
        }

        // GET: B2bPrepaidOffer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var b2bPrepaidOffer = await _context.B2bPrepaidOffer.FindAsync(id);
            if (b2bPrepaidOffer == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", b2bPrepaidOffer.CreatedById);
            return View(b2bPrepaidOffer);
        }

        // POST: B2bPrepaidOffer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, B2bPrepaidOffer b2bPrepaidOffer)
        {
            if (id != b2bPrepaidOffer.Id)
            {
                return NotFound();
            }

           
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    b2bPrepaidOffer.CreatedOn = DateTime.Now;
                    b2bPrepaidOffer.CreatedById = userId;
                    _context.Update(b2bPrepaidOffer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!B2bPrepaidOfferExists(b2bPrepaidOffer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
          
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", b2bPrepaidOffer.CreatedById);
            return View(b2bPrepaidOffer);
        }

        // GET: B2bPrepaidOffer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var b2bPrepaidOffer = await _context.B2bPrepaidOffer
                .Include(b => b.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (b2bPrepaidOffer == null)
            {
                return NotFound();
            }

            return View(b2bPrepaidOffer);
        }

        // POST: B2bPrepaidOffer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var b2bPrepaidOffer = await _context.B2bPrepaidOffer.FindAsync(id);
            if (b2bPrepaidOffer != null)
            {
                _context.B2bPrepaidOffer.Remove(b2bPrepaidOffer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool B2bPrepaidOfferExists(int id)
        {
            return _context.B2bPrepaidOffer.Any(e => e.Id == id);
        }
    }
}
