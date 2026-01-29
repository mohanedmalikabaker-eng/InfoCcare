using InfoCcare.Data;
using InfoCcare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoCcare.Controllers
{
    public class TaktikPostpaidViewModelController : Controller
    {

        private readonly ApplicationDbContext _context;

        public TaktikPostpaidViewModelController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new TaktikPostpaidOfferViewModel
            {
                Offers = await _context.TaktikPostpaid
                    .OrderBy(o => o.Id)
                    .ToListAsync(),

                Descriptions = await _context.Descriptions
                    .Include(d => d.Segment)
                    .Where(d => d.IsActive && d.Segment.Title == "TaktikPostpaid")
                    .ToListAsync()
            };

            return View(model);
        }
    }
}
