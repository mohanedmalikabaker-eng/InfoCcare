using InfoCcare.Data;
using InfoCcare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoCcare.Controllers
{
    public class MazayaOfferViewModelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MazayaOfferViewModelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: B2b Prepaid Offer
        public IActionResult MazayaOffer()
        {
            var viewModel = new MazayaOfferViewModel
            {
                Offers = _context.Mazaya.ToList(),
                Descriptions = _context.Descriptions
                    .Include(d => d.Segment)
                    .Where(d => d.IsActive && d.Segment.Title == "Mazaya")
                    .ToList()
            };

            return View(viewModel);
        }
    }
}
