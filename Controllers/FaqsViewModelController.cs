using InfoCcare.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoCcare.Controllers
{
    public class FaqsViewModelController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FaqsViewModelController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Faqs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Faqs.Include(f => f.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }
    }
}
