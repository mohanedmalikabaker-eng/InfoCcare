using InfoCcare.Data;
using InfoCcare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InfoCcare.Controllers
{
    public class DevicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DevicesController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Devices
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Device.Include(d => d.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Devices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var device = await _context.Device
                .Include(d => d.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (device == null) return NotFound();

            return View(device);
        }

        // GET: Devices/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: Devices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Device device)
        {
            if (!ModelState.IsValid)
            {
                ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", device.CreatedById);
                return View(device);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            device.CreatedOn = DateTime.Now;
            device.CreatedById = userId ?? "";

            // Upload Image
            if (device.ImageFile != null && device.ImageFile.Length > 0)
            {
                var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                var ext = Path.GetExtension(device.ImageFile.FileName).ToLowerInvariant();

                if (!allowed.Contains(ext))
                {
                    ModelState.AddModelError("ImageFile", "Allowed: jpg, jpeg, png, webp");
                    ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", device.CreatedById);
                    return View(device);
                }

                var folder = Path.Combine(_env.WebRootPath, "uploads", "devices");
                Directory.CreateDirectory(folder);

                var fileName = $"{Guid.NewGuid()}{ext}";
                var fullPath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await device.ImageFile.CopyToAsync(stream);
                }

                device.Img = $"/uploads/devices/{fileName}";
            }

            _context.Add(device);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Devices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var device = await _context.Device.FindAsync(id);
            if (device == null) return NotFound();

            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", device.CreatedById);
            return View(device);
        }

        // POST: Devices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Device device)
        {
            if (id != device.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", device.CreatedById);
                return View(device);
            }

            // Get old record to keep Img if no new image uploaded
            var old = await _context.Device.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (old == null) return NotFound();

            device.Img = old.Img;

            // Upload new image (optional)
            if (device.ImageFile != null && device.ImageFile.Length > 0)
            {
                var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                var ext = Path.GetExtension(device.ImageFile.FileName).ToLowerInvariant();

                if (!allowed.Contains(ext))
                {
                    ModelState.AddModelError("ImageFile", "Allowed: jpg, jpeg, png, webp");
                    ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Name", device.CreatedById);
                    return View(device);
                }

                var folder = Path.Combine(_env.WebRootPath, "uploads", "devices");
                Directory.CreateDirectory(folder);

                var fileName = $"{Guid.NewGuid()}{ext}";
                var fullPath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await device.ImageFile.CopyToAsync(stream);
                }

                device.Img = $"/uploads/devices/{fileName}";

                // (اختياري) حذف الصورة القديمة من السيرفر
                // if (!string.IsNullOrWhiteSpace(old.Img))
                // {
                //     var oldPath = Path.Combine(_env.WebRootPath, old.Img.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
                //     if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                // }
            }

            try
            {
                // لو ما داير تغيّر CreatedOn/CreatedBy في Edit، احذف السطرين الجايين
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                device.CreatedOn = old.CreatedOn;      // أفضل نحافظ على القديم
                device.CreatedById = old.CreatedById;  // أفضل نحافظ على القديم

                _context.Update(device);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(device.Id)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Devices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var device = await _context.Device
                .Include(d => d.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (device == null) return NotFound();

            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var device = await _context.Device.FindAsync(id);
            if (device != null)
            {
                _context.Device.Remove(device);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceExists(int id)
        {
            return _context.Device.Any(e => e.Id == id);
        }
    }
}
