using InfoCcare.Data;
using InfoCcare.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoCare.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly ApplicationDbContext _context;

        public UserController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }

        // 1️⃣ List Users
        public IActionResult Index(string search, int page = 1)
        {
            int pageSize = 10;

            var usersQuery = _userManager.Users
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                usersQuery = usersQuery.Where(u =>
                    EF.Functions.Like(u.UserName!, $"%{search}%") ||
                    EF.Functions.Like(u.Email!, $"%{search}%") ||
                    EF.Functions.Like(u.Name!, $"%{search}%"));
            }

            int totalUsers = usersQuery.Count();

            var users = usersQuery
                .OrderBy(u => u.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages =
                (int)Math.Ceiling(totalUsers / (double)pageSize);

            ViewBag.Search = search;

            return View(users);
        }



        // 2️⃣ Create User (GET)
        public IActionResult Create()
        {
            return View();
        }

        // 3️⃣ Create User (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationUser model, string password)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.UserName = model.Email;
            model.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(model, password);

            if (result.Succeeded)
                return RedirectToAction(nameof(Index));

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);
            return View(model);
        }

        // 4️⃣ Edit User (GET)
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // 5️⃣ Edit User (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUser model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
                return NotFound();
            user.Name = model.Name;
            user.Location = model.Location;
            user.Department= model.Department;

            await _userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }

        // 6️⃣ Delete User
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }

        // 7️⃣ Assign Role (GET)
        public async Task<IActionResult> AssignRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            // كل الـ Roles الموجودة (Admin / User)
            ViewBag.Roles = _roleManager.Roles.ToList();

            return View(user);
        }

        // 8️⃣ Assign Role (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            // إزالة كل الأدوار الحالية
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            // إضافة الرول المختار
            await _userManager.AddToRoleAsync(user, role);

            return RedirectToAction(nameof(Index));
        }

        // 9️⃣ Reset Password (GET)
        public async Task<IActionResult> ResetPassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // 🔐 Reset Password (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string id, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            // توليد Token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // تعيين Password جديد
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (result.Succeeded)
                return RedirectToAction(nameof(Index));

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(user);
        }


    }
}
