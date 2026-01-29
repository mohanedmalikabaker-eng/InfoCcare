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
    public class PostpaidOffersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostpaidOffersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PostpaidOffers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PostpaidOffers.Include(p => p.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PostpaidOffers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postpaidOffer = await _context.PostpaidOffers
                .Include(p => p.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postpaidOffer == null)
            {
                return NotFound();
            }

            return View(postpaidOffer);
        }

        // GET: PostpaidOffers/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: PostpaidOffers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostpaidOffer postpaidOffer)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            postpaidOffer.CreatedOn = DateTime.Now;
            postpaidOffer.CreatedById = userId;
            _context.Add(postpaidOffer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", postpaidOffer.CreatedById);
            return View(postpaidOffer);
        }

        // GET: PostpaidOffers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postpaidOffer = await _context.PostpaidOffers.FindAsync(id);
            if (postpaidOffer == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", postpaidOffer.CreatedById);
            return View(postpaidOffer);
        }

        // POST: PostpaidOffers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,PostpaidOffer postpaidOffer)
        {
            if (id != postpaidOffer.Id)
            {
                return NotFound();
            }

           
                try
                {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                postpaidOffer.CreatedOn = DateTime.Now;
                postpaidOffer.CreatedById = userId;
                _context.Update(postpaidOffer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostpaidOfferExists(postpaidOffer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
           
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", postpaidOffer.CreatedById);
            return View(postpaidOffer);
        }

        // GET: PostpaidOffers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postpaidOffer = await _context.PostpaidOffers
                .Include(p => p.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postpaidOffer == null)
            {
                return NotFound();
            }

            return View(postpaidOffer);
        }

        // POST: PostpaidOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postpaidOffer = await _context.PostpaidOffers.FindAsync(id);
            if (postpaidOffer != null)
            {
                _context.PostpaidOffers.Remove(postpaidOffer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostpaidOfferExists(int id)
        {
            return _context.PostpaidOffers.Any(e => e.Id == id);
        }
    }
}
