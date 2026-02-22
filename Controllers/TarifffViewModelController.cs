using InfoCcare.Data;
using InfoCcare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoCcare.Controllers
{
    public class TarifffViewModelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TarifffViewModelController(ApplicationDbContext context)
        {
            _context = context;
        }

        //PostPaid
        public async Task<IActionResult> PostpaidTariffs()
        {
            var PostpaidTarifff = await _context.Tarifff
                .Where(t => t.Segment.Title == "Postpaid")
                .ToListAsync();

            var calls = PostpaidTarifff
                .Where(t => t.CallSms.Trim().StartsWith("المكالمات"))
                .ToList();

            var sms = PostpaidTarifff
                .Where(t => t.CallSms.StartsWith("رسالة") || t.CallSms.StartsWith("الرسائل"))
                .ToList();

            var vm = new TarifView
            {
                Call = calls,
                Sms = sms
            };

            return View(vm);
        }

        //Prepaid
        public async Task<IActionResult> PrepaidTariffs()
        {
            var PrepaidTarifff = await _context.Tarifff
                .Where(t => t.Segment.Title == "Prepaid")
                .ToListAsync();

            var calls = PrepaidTarifff
                .Where(t => t.CallSms.Trim().StartsWith("المكالمات"))
                .ToList();

            var sms = PrepaidTarifff
                .Where(t => t.CallSms.StartsWith("رسالة") || t.CallSms.StartsWith("الرسائل"))
                .ToList();

            var vm = new TarifView
            {
                Call = calls,
                Sms = sms
            };

            return View(vm);
        }

        //B2BPrepaid
        public async Task<IActionResult> B2BPrepaidTariffs()
        {
            var B2BPrepaidTarifff = await _context.Tarifff
                .Where(t => t.Segment.Title == "B2BPrepaid")
                .ToListAsync();

            var calls = B2BPrepaidTarifff
                .Where(t => t.CallSms.Trim().StartsWith("المكالمات"))
                .ToList();

            var sms = B2BPrepaidTarifff
                .Where(t => t.CallSms.StartsWith("رسالة") || t.CallSms.StartsWith("الرسائل"))
                .ToList();

            var vm = new TarifView
            {
                Call = calls,
                Sms = sms
            };

            return View(vm);
        }

        //B2BPostpaid
        public async Task<IActionResult> B2BPostpaidTariffs()
        {
            var B2BPostpaidTarifff = await _context.Tarifff
                .Where(t => t.Segment.Title == "B2BPostpaid")
                .ToListAsync();

            var calls = B2BPostpaidTarifff
                .Where(t => t.CallSms.Trim().StartsWith("المكالمات"))
                .ToList();

            var sms = B2BPostpaidTarifff
                .Where(t => t.CallSms.StartsWith("رسالة") || t.CallSms.StartsWith("الرسائل"))
                .ToList();

            var cug = B2BPostpaidTarifff
               .Where(t => t.CallSms.StartsWith("CUG") || t.CallSms.StartsWith("cug"))
               .ToList();

            var vm = new TarifView
            {
                Call = calls,
                Sms = sms
            };

            return View(vm);
        }

        //Mazaya
        public async Task<IActionResult> MazayaTariffs()
        {
            var MazayaTarifffs = await _context.Tarifff
                .Where(t => t.Segment.Title == "Mazaya")
                .ToListAsync();

            var calls = MazayaTarifffs
                .Where(t => t.CallSms.Trim().StartsWith("المكالمات"))
                .ToList();

            var sms = MazayaTarifffs
                .Where(t => t.CallSms.StartsWith("رسالة") || t.CallSms.StartsWith("الرسائل"))
                .ToList();


            var vm = new TarifView
            {
                Call = calls,
                Sms = sms
            };

            return View(vm);
        }
    }
}
