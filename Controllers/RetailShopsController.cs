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
    public class RetailShopsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RetailShopsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RetailShops
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RetailShops.Include(r => r.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RetailShops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var retailShop = await _context.RetailShops
                .Include(r => r.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (retailShop == null)
            {
                return NotFound();
            }

            return View(retailShop);
        }

        // GET: RetailShops/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: RetailShops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RetailShop retailShop)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            retailShop.CreatedOn = DateTime.Now;
            retailShop.CreatedById = userId;
            _context.Add(retailShop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", retailShop.CreatedById);
            return View(retailShop);
        }

        // GET: RetailShops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var retailShop = await _context.RetailShops.FindAsync(id);
            if (retailShop == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Id", retailShop.CreatedById);
            return View(retailShop);
        }

        // POST: RetailShops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,RetailShop retailShop)
        {
            if (id != retailShop.Id)
            {
                return NotFound();
            }

            
                try
                {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                retailShop.CreatedOn = DateTime.Now;
                retailShop.CreatedById = userId;
                _context.Update(retailShop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RetailShopExists(retailShop.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
         
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", retailShop.CreatedById);
            return View(retailShop);
        }

        // GET: RetailShops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var retailShop = await _context.RetailShops
                .Include(r => r.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (retailShop == null)
            {
                return NotFound();
            }

            return View(retailShop);
        }

        // POST: RetailShops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var retailShop = await _context.RetailShops.FindAsync(id);
            if (retailShop != null)
            {
                _context.RetailShops.Remove(retailShop);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RetailShopExists(int id)
        {
            return _context.RetailShops.Any(e => e.Id == id);
        }
    }
}
