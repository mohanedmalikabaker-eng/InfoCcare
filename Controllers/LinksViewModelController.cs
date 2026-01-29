using InfoCcare.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoCcare.Controllers
{
    public class LinksViewModelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LinksViewModelController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Links
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Links.Include(l => l.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }
    }
}
