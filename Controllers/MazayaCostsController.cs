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
    public class MazayaCostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MazayaCostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MazayaCosts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MazayaCost.Include(m => m.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MazayaCosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mazayaCost = await _context.MazayaCost
                .Include(m => m.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mazayaCost == null)
            {
                return NotFound();
            }

            return View(mazayaCost);
        }

        // GET: MazayaCosts/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: MazayaCosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MazayaCost mazayaCost)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            mazayaCost.CreatedOn = DateTime.Now;
            mazayaCost.CreatedById = userId;
            _context.Add(mazayaCost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
          
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", mazayaCost.CreatedById);
            return View(mazayaCost);
        }

        // GET: MazayaCosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mazayaCost = await _context.MazayaCost.FindAsync(id);
            if (mazayaCost == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", mazayaCost.CreatedById);
            return View(mazayaCost);
        }

        // POST: MazayaCosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlanName,OfferPrice,SimPrice,Cl,PriceVatInclude,CreatedOn,CreatedById")] MazayaCost mazayaCost)
        {
            if (id != mazayaCost.Id)
            {
                return NotFound();
            }

           
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    mazayaCost.CreatedOn = DateTime.Now;
                    mazayaCost.CreatedById = userId;
                    _context.Update(mazayaCost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MazayaCostExists(mazayaCost.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", mazayaCost.CreatedById);
            return View(mazayaCost);
        }

        // GET: MazayaCosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mazayaCost = await _context.MazayaCost
                .Include(m => m.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mazayaCost == null)
            {
                return NotFound();
            }

            return View(mazayaCost);
        }

        // POST: MazayaCosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mazayaCost = await _context.MazayaCost.FindAsync(id);
            if (mazayaCost != null)
            {
                _context.MazayaCost.Remove(mazayaCost);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MazayaCostExists(int id)
        {
            return _context.MazayaCost.Any(e => e.Id == id);
        }
    }
}
