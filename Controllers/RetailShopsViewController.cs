using InfoCcare.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoCcare.Controllers
{
    public class RetailShopsViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RetailShopsViewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RetailShops
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RetailShops.Include(r => r.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }
    }
}
