using InfoCcare.Data;
using InfoCcare.Models;
using InfoCcare.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoCcare.Controllers
{
    public class UserActivityLogController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserActivityLogController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /AuditLogs/Index?email=&from=&to=&page=1&pageSize=20

        public async Task<IActionResult> Index(string? email, string? entity, DateTime? from, DateTime? to, int page = 1, int pageSize = 20)
        {
            if (page < 1) page = 1;
            if (pageSize < 5) pageSize = 5;
            if (pageSize > 100) pageSize = 100;

            var query = _context.AuditLogs.AsNoTracking().AsQueryable();

            // Filter by Email
            if (!string.IsNullOrWhiteSpace(email))
                query = query.Where(x => x.UserEmail.Contains(email));

            // Filter by Entity
            if (!string.IsNullOrWhiteSpace(entity))
                query = query.Where(x => x.EntityName.Contains(entity));

            // Filter by From
            if (from.HasValue)
                query = query.Where(x => x.TimesTamp >= from.Value);

            // Filter by To (inclusive end of day)
            if (to.HasValue)
            {
                var toEndOfDay = to.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(x => x.TimesTamp <= toEndOfDay);
            }

            var totalCount = await query.CountAsync();

            var logs = await query
                .OrderByDescending(x => x.TimesTamp)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var vm = new UserActivityLog
            {
                Email = email,
                Entity = entity,
                From = from,
                To = to,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                Logs = logs
            };

            if (vm.TotalPages > 0 && vm.Page > vm.TotalPages)
            {
                return RedirectToAction(nameof(Index), new
                {
                    email,
                    entity,
                    from,
                    to,
                    page = vm.TotalPages,
                    pageSize
                });
            }

            return View(vm);
        }

        public async Task<IActionResult> Details(int id)
        {
            var log = await _context.AuditLogs.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (log == null) return NotFound();

            return View(log);
        }
    }
}
