using InfoCcare.Data;
using InfoCcare.Models;
using Microsoft.AspNetCore.Mvc;

namespace InfoCcare.Controllers
{
    public class PrepaidDataViewModelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrepaidDataViewModelController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var dataList = _context.PrePaid_Data.ToList();
            var descriptions = _context.Descriptions
            .Where(d => d.Segment.Title == "PrePaid_Data" && d.IsActive)
            .ToList();

            var viewModel = new PrepaidDataViewModel
            {
                DataList = dataList,
                Descriptions = descriptions
            };

            return View(viewModel);
        }
    }
}
