using InfoCcare.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoCcare.Controllers
{
    public class BadeFeesViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BadeFeesViewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BadeFees
        public async Task<IActionResult> BadeFees()
        {
            var applicationDbContext = _context.BadeFees.Include(b => b.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }
    }
}
