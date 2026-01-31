using InfoCcare.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoCcare.Controllers
{
    public class MazayaCostsViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MazayaCostsViewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MazayaCosts
        public async Task<IActionResult> MazayaCosts()
        {
            var applicationDbContext = _context.MazayaCost.Include(m => m.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }
    }
}
