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
    public class DealersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DealersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dealers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Dealers.Include(d => d.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Dealers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dealer = await _context.Dealers
                .Include(d => d.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dealer == null)
            {
                return NotFound();
            }

            return View(dealer);
        }

        // GET: Dealers/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: Dealers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Dealer dealer)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            dealer.CreatedOn = DateTime.Now;
            dealer.CreatedById = userId;
            _context.Add(dealer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
          
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", dealer.CreatedById);
            return View(dealer);
        }

        // GET: Dealers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dealer = await _context.Dealers.FindAsync(id);
            if (dealer == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", dealer.CreatedById);
            return View(dealer);
        }

        // POST: Dealers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Dealer dealer)
        {
            if (id != dealer.Id)
            {
                return NotFound();
            }

            
                try
                {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                dealer.CreatedOn = DateTime.Now;
                dealer.CreatedById = userId;
                _context.Update(dealer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DealerExists(dealer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
               
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", dealer.CreatedById);
            return View(dealer);
        }

        // GET: Dealers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dealer = await _context.Dealers
                .Include(d => d.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dealer == null)
            {
                return NotFound();
            }

            return View(dealer);
        }

        // POST: Dealers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dealer = await _context.Dealers.FindAsync(id);
            if (dealer != null)
            {
                _context.Dealers.Remove(dealer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DealerExists(int id)
        {
            return _context.Dealers.Any(e => e.Id == id);
        }
    }
}
