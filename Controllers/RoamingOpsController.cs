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
    public class RoamingOpsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoamingOpsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RoamingOps
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RoamingOp.Include(r => r.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RoamingOps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roamingOp = await _context.RoamingOp
                .Include(r => r.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roamingOp == null)
            {
                return NotFound();
            }

            return View(roamingOp);
        }

        // GET: RoamingOps/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: RoamingOps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoamingOp roamingOp)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            roamingOp.CreatedOn = DateTime.Now;
            roamingOp.CreatedById = userId;
            _context.Add(roamingOp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", roamingOp.CreatedById);
            return View(roamingOp);
        }

        // GET: RoamingOps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roamingOp = await _context.RoamingOp.FindAsync(id);
            if (roamingOp == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", roamingOp.CreatedById);
            return View(roamingOp);
        }

        // POST: RoamingOps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,RoamingOp roamingOp)
        {
            if (id != roamingOp.Id)
            {
                return NotFound();
            }


                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    roamingOp.CreatedOn = DateTime.Now;
                    roamingOp.CreatedById = userId;
                    _context.Update(roamingOp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoamingOpExists(roamingOp.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
              
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", roamingOp.CreatedById);
            return View(roamingOp);
        }

        // GET: RoamingOps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roamingOp = await _context.RoamingOp
                .Include(r => r.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roamingOp == null)
            {
                return NotFound();
            }

            return View(roamingOp);
        }

        // POST: RoamingOps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roamingOp = await _context.RoamingOp.FindAsync(id);
            if (roamingOp != null)
            {
                _context.RoamingOp.Remove(roamingOp);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoamingOpExists(int id)
        {
            return _context.RoamingOp.Any(e => e.Id == id);
        }
    }
}
