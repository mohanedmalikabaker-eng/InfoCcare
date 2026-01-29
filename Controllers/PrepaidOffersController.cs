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
    public class PrepaidOffersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrepaidOffersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PrepaidOffers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PrepaidOffers.Include(p => p.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PrepaidOffers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prepaidOffer = await _context.PrepaidOffers
                .Include(p => p.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prepaidOffer == null)
            {
                return NotFound();
            }

            return View(prepaidOffer);
        }

        // GET: PrepaidOffers/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: PrepaidOffers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PrepaidOffer prepaidOffer)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            prepaidOffer.CreatedOn = DateTime.Now;
            prepaidOffer.CreatedById = userId;
            _context.Add(prepaidOffer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", prepaidOffer.CreatedById);
            return View(prepaidOffer);
        }

        // GET: PrepaidOffers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prepaidOffer = await _context.PrepaidOffers.FindAsync(id);
            if (prepaidOffer == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", prepaidOffer.CreatedById);
            return View(prepaidOffer);
        }

        // POST: PrepaidOffers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,PrepaidOffer prepaidOffer)
        {
            if (id != prepaidOffer.Id)
            {
                return NotFound();
            }


            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                prepaidOffer.CreatedOn = DateTime.Now;
                prepaidOffer.CreatedById = userId;
                _context.Update(prepaidOffer);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrepaidOfferExists(prepaidOffer.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
                return RedirectToAction(nameof(Index));
           
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", prepaidOffer.CreatedById);
            return View(prepaidOffer);
        }

        // GET: PrepaidOffers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prepaidOffer = await _context.PrepaidOffers
                .Include(p => p.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prepaidOffer == null)
            {
                return NotFound();
            }

            return View(prepaidOffer);
        }

        // POST: PrepaidOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prepaidOffer = await _context.PrepaidOffers.FindAsync(id);
            if (prepaidOffer != null)
            {
                _context.PrepaidOffers.Remove(prepaidOffer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrepaidOfferExists(int id)
        {
            return _context.PrepaidOffers.Any(e => e.Id == id);
        }
    }
}
