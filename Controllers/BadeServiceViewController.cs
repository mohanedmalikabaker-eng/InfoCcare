using InfoCcare.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoCcare.Controllers
{
    public class BadeServiceViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BadeServiceViewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bades
        public async Task<IActionResult> BadeService()
        {
            var applicationDbContext = _context.Bade.Include(b => b.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

    }
}
