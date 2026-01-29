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
    public class FaqsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FaqsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Faqs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Faqs.Include(f => f.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Faqs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faqs = await _context.Faqs
                .Include(f => f.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faqs == null)
            {
                return NotFound();
            }

            return View(faqs);
        }

        // GET: Faqs/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: Faqs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Faqs faqs)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            faqs.CreatedOn = DateTime.Now;
            faqs.CreatedById = userId;
            _context.Add(faqs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
     
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", faqs.CreatedById);
            return View(faqs);
        }

        // GET: Faqs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faqs = await _context.Faqs.FindAsync(id);
            if (faqs == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", faqs.CreatedById);
            return View(faqs);
        }

        // POST: Faqs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Faqs faqs)
        {
            if (id != faqs.Id)
            {
                return NotFound();
            }

           
                try
                {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                faqs.CreatedOn = DateTime.Now;
                faqs.CreatedById = userId;
                _context.Update(faqs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaqsExists(faqs.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
           
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", faqs.CreatedById);
            return View(faqs);
        }

        // GET: Faqs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faqs = await _context.Faqs
                .Include(f => f.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faqs == null)
            {
                return NotFound();
            }

            return View(faqs);
        }

        // POST: Faqs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faqs = await _context.Faqs.FindAsync(id);
            if (faqs != null)
            {
                _context.Faqs.Remove(faqs);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaqsExists(int id)
        {
            return _context.Faqs.Any(e => e.Id == id);
        }
    }
}
