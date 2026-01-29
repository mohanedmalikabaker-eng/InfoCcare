using InfoCcare.Data;
using InfoCcare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoCcare.Controllers
{
    public class TaktikB2BPrepaidOfferViewModelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaktikB2BPrepaidOfferViewModelController(ApplicationDbContext context)
        {
            _context = context;
        }

        //B2B Prepaid
        public async Task<IActionResult> TaktikB2BPrepaid()
        {
            var model = new TaktikB2BPrepaidOfferViewModel
            {
                Offers = await _context.TaktikB2BPrepaid
                    .OrderBy(o => o.FlexType)
                    .ToListAsync(),

                Descriptions = await _context.Descriptions
                    .Include(d => d.Segment)
                    .Where(d => d.IsActive && d.Segment.Title == "TaktikB2BPrepaid")
                    .ToListAsync()
            };

            return View(model);
        }
    }
}
