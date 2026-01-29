using InfoCcare.Data;
using InfoCcare.Models;
using Microsoft.AspNetCore.Mvc;

namespace InfoCcare.Controllers
{
    public class PrepaidOfferViewModelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrepaidOfferViewModelController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var offers = _context.PrepaidOffers.ToList();

            var descriptions = _context.Descriptions
                .Where(d => d.Segment.Title == "Prepaid" && d.IsActive)
                .ToList();

            var viewModel = new PrepaidOfferViewModel
            {
                Offers = offers,
                Descriptions = descriptions
            };

            return View(viewModel);
        }
    }
}
