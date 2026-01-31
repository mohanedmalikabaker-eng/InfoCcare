using InfoCcare.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoCcare.Controllers
{
    public class MazayaExtraUnitsViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MazayaExtraUnitsViewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MazayaExtraUnits
        public async Task<IActionResult> MazayaExtraUnits()
        {
            var applicationDbContext = _context.MazayaExtraUnits.Include(m => m.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }
    }
}
