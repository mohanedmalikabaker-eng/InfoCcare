using InfoCcare.Data;
using InfoCcare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoCcare.Controllers
{
    public class TaktikB2BPostpaidOfferViewModelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaktikB2BPostpaidOfferViewModelController(ApplicationDbContext context)
        {
            _context = context;
        }

        //B2B Prepaid
        public async Task<IActionResult> TaktikB2BPostpaid()
        {
            var model = new TaktikB2BPostpaidOfferViewModel
            {
                Offers = await _context.TaktikB2BPostpaid
                    .OrderBy(o => o.FlexType)
                    .ToListAsync(),

                Descriptions = await _context.Descriptions
                    .Include(d => d.Segment)
                    .Where(d => d.IsActive && d.Segment.Title == "TaktikB2BPostpaid")
                    .ToListAsync()
            };

            return View(model);
        }
    }
}
