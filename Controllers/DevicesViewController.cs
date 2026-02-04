using InfoCcare.Data;
using InfoCcare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoCcare.Controllers
{
    public class DevicesViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DevicesViewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DevicesView/Devices
        public async Task<IActionResult> Devices()
        {
            var devices = await _context.Device
                .AsNoTracking()
                .ToListAsync();

            return View(devices);
        }

    }
}
