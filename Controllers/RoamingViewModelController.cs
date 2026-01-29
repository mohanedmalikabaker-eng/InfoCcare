using InfoCcare.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoCcare.Controllers
{
    public class RoamingViewModelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoamingViewModelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Roaming Tariff Vat&WNoVat
        public async Task<IActionResult> RoamingTariff()
        {
            var applicationDbContext = _context.Roaming.Include(r => r.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: RoamingOps
        public async Task<IActionResult> RoamingOps()
        {
            var applicationDbContext = _context.RoamingOp.Include(r => r.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Zones
        public async Task<IActionResult> Zones()
        {
            var applicationDbContext = _context.Zones.Include(z => z.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }
    }
}
