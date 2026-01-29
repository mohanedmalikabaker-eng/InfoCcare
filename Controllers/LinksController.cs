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
    public class LinksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LinksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Links
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Links.Include(l => l.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Links/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var link = await _context.Links
                .Include(l => l.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (link == null)
            {
                return NotFound();
            }

            return View(link);
        }

        // GET: Links/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: Links/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Link link)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            link.CreatedOn = DateTime.Now;
            link.CreatedById = userId;
            _context.Add(link);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", link.CreatedById);
            return View(link);
        }

        // GET: Links/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var link = await _context.Links.FindAsync(id);
            if (link == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", link.CreatedById);
            return View(link);
        }

        // POST: Links/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Link link)
        {
            if (id != link.Id)
            {
                return NotFound();
            }

           
                try
                {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                link.CreatedOn = DateTime.Now;
                link.CreatedById = userId;
                _context.Update(link);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LinkExists(link.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
          
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", link.CreatedById);
            return View(link);
        }

        // GET: Links/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var link = await _context.Links
                .Include(l => l.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (link == null)
            {
                return NotFound();
            }

            return View(link);
        }

        // POST: Links/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var link = await _context.Links.FindAsync(id);
            if (link != null)
            {
                _context.Links.Remove(link);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LinkExists(int id)
        {
            return _context.Links.Any(e => e.Id == id);
        }
    }
}
