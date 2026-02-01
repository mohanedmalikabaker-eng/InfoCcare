using InfoCcare.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoCcare.Controllers
{
    public class BaseTranLimitsViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BaseTranLimitsViewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BaseTranLimits
        public async Task<IActionResult> BadeTranLimit()
        {
            var applicationDbContext = _context.BaseTranLimits.Include(b => b.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

    }
}
