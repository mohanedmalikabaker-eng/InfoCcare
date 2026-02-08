using InfoCcare.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoCcare.Controllers
{
    public class DeviceDescPricesViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeviceDescPricesViewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DeviceDescPrices
        public async Task<IActionResult> DeviceDescPrices()
        {
            var applicationDbContext = _context.DeviceDescPrice.Include(d => d.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }
    }
}
