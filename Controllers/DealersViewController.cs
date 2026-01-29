using InfoCcare.Data;
using InfoCcare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InfoCcare.Controllers
{
    public class DealersViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DealersViewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dealers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Dealers.Include(d => d.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

    } 
}
