using InfoCcare.Data;
using InfoCcare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoCcare.Controllers
{
    public class TaktikPrepaidViewModelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaktikPrepaidViewModelController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new TaktikPrepaidOfferViewModel
            {
                Offers = await _context.TaktikPrepaids
                    .OrderBy(o => o.FlexType)
                    .ToListAsync(),

                Descriptions = await _context.Descriptions
                    .Include(d => d.Segment)
                    .Where(d => d.IsActive && d.Segment.Title == "TaktikPrepaid")
                    .ToListAsync()
            };

            return View(model);
        }
    }
}
