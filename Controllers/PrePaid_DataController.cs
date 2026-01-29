using InfoCcare.Data;
using InfoCcare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InfoCcare.Controllers
{
    public class PrePaid_DataController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrePaid_DataController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PrePaid_Data
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PrePaid_Data.Include(p => p.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PrePaid_Data/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prePaid_Data = await _context.PrePaid_Data
                .Include(p => p.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prePaid_Data == null)
            {
                return NotFound();
            }

            return View(prePaid_Data);
        }

        // GET: PrePaid_Data/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: PrePaid_Data/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PrePaid_Data prePaid_Data)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            prePaid_Data.CreatedOn = DateTime.Now;
            prePaid_Data.CreatedById = userId;
            _context.Add(prePaid_Data);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", prePaid_Data.CreatedById);
            return View(prePaid_Data);
        }

        // GET: PrePaid_Data/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prePaid_Data = await _context.PrePaid_Data.FindAsync(id);
            if (prePaid_Data == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", prePaid_Data.CreatedById);
            return View(prePaid_Data);
        }

        // POST: PrePaid_Data/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,PrePaid_Data prePaid_Data)
        {
            if (id != prePaid_Data.Id)
            {
                return NotFound();
            }

           
                try
                {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                prePaid_Data.CreatedOn = DateTime.Now;
                prePaid_Data.CreatedById = userId;
                _context.Update(prePaid_Data);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrePaid_DataExists(prePaid_Data.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
       
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Id", prePaid_Data.CreatedById);
            return View(prePaid_Data);
        }

        // GET: PrePaid_Data/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prePaid_Data = await _context.PrePaid_Data
                .Include(p => p.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prePaid_Data == null)
            {
                return NotFound();
            }

            return View(prePaid_Data);
        }

        // POST: PrePaid_Data/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prePaid_Data = await _context.PrePaid_Data.FindAsync(id);
            if (prePaid_Data != null)
            {
                _context.PrePaid_Data.Remove(prePaid_Data);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrePaid_DataExists(int id)
        {
            return _context.PrePaid_Data.Any(e => e.Id == id);
        }
    }
}
