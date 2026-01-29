using InfoCcare.Data;
using InfoCcare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoCcare.Controllers
{
    public class PostpaidOfferViewModelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostpaidOfferViewModelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Postpaid Offer
        public IActionResult Index()
        {
            var viewModel = new PostpaidOfferViewModel
            {
                Offers = _context.PostpaidOffers.ToList(),
                Descriptions = _context.Descriptions
                    .Include(d => d.Segment)
                    .Where(d => d.IsActive && d.Segment.Title == "Postpaid")
                    .ToList()
            };

            return View(viewModel);
        }
    }
}
