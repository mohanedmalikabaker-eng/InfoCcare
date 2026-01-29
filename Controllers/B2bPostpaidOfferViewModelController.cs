using InfoCcare.Data;
using InfoCcare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoCcare.Controllers
{
    public class B2bPostpaidOfferViewModelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public B2bPostpaidOfferViewModelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Beb Postpaid Offer

        public IActionResult B2bPostpaidOffer()
        {
            var viewModel = new B2bPostpaidOfferViewModel
            {
                Offers = _context.B2bPostpaidOffer.ToList(),
                Descriptions = _context.Descriptions
                    .Include(d => d.Segment)
                    .Where(d => d.IsActive && d.Segment.Title == "B2BPostpaid")
                    .ToList()
            };

            return View(viewModel);
        }

        // GET: DataCard&Extra
        public IActionResult DataCardExtra()
        {
            var viewModel = new B2bPostpaidOfferViewModel
            {
                Offers = _context.B2bPostpaidOffer.ToList(),
                Descriptions = _context.Descriptions
                    .Include(d => d.Segment)
                    .Where(d => d.IsActive && d.Segment.Title == "B2BPostpaidDataCard")
                    .ToList()
            };

            return View(viewModel);
        }
    }

}

