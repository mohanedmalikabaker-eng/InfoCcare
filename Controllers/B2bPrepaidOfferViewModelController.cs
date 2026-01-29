using InfoCcare.Data;
using InfoCcare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoCcare.Controllers
{
    public class B2bPrepaidOfferViewModelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public B2bPrepaidOfferViewModelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: B2b Prepaid Offer
        public IActionResult B2bPrepaidOffer()
        {
            var viewModel = new B2bPrepaidOfferViewModel
            {
                Offers = _context.B2bPrepaidOffer.ToList(),
                Descriptions = _context.Descriptions
                    .Include(d => d.Segment)
                    .Where(d => d.IsActive && d.Segment.Title == "B2BPrepaid")
                    .ToList()
            };

            return View(viewModel);
        }

        // GET: DataApnCard
        public IActionResult DataApnCard()
        {
            var viewModel = new B2bPrepaidOfferViewModel
            {
                Offers = _context.B2bPrepaidOffer.ToList(),
                Descriptions = _context.Descriptions
                    .Include(d => d.Segment)
                    .Where(d => d.IsActive && d.Segment.Title == "B2BPrepaid")
                    .ToList()
            };

            return View(viewModel);
        }
    }
}
