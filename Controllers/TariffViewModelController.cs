using InfoCcare.Data;
using InfoCcare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoCcare.Controllers
{
    public class TariffViewModelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TariffViewModelController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> PostpaidTariffs()
        {
            var PostpaidTariff = await _context.Tariff
                .Where(t => t.Segment.Title == "Postpaid")
                .ToListAsync();

            var calls = PostpaidTariff
                .Where(t => t.CallSms.Trim().StartsWith("المكالمات"))
                .ToList();

            var sms = PostpaidTariff
                .Where(t => t.CallSms.StartsWith("رسالة") || t.CallSms.StartsWith("الرسائل"))
                .ToList();

            var vm = new TariffViewModel
            {
                Call = calls,
                Sms = sms
            };

            return View(vm);
        }
        //Prepaid
        public async Task<IActionResult> PrepaidTariffs()
        {
            var PrepaidTariff = await _context.Tariff
                .Where(t => t.Segment.Title == "Prepaid")
                .ToListAsync();

            var calls = PrepaidTariff
                .Where(t => t.CallSms.Trim().StartsWith("المكالمات"))
                .ToList();

            var sms = PrepaidTariff
                .Where(t => t.CallSms.StartsWith("رسالة") || t.CallSms.StartsWith("الرسائل"))
                .ToList();

            var vm = new TariffViewModel
            {
                Call = calls,
                Sms = sms
            };

            return View(vm);
        }

        //B2BPrepaid
        public async Task<IActionResult> B2BPrepaidTariffs()
        {
            var B2BPrepaidTariff = await _context.Tariff
                .Where(t => t.Segment.Title == "B2BPrepaid")
                .ToListAsync();

            var calls = B2BPrepaidTariff
                .Where(t => t.CallSms.Trim().StartsWith("المكالمات"))
                .ToList();

            var sms = B2BPrepaidTariff
                .Where(t => t.CallSms.StartsWith("رسالة") || t.CallSms.StartsWith("الرسائل"))
                .ToList();

            var vm = new TariffViewModel
            {
                Call = calls,
                Sms = sms
            };

            return View(vm);
        }

        //B2BPostpaid
        public async Task<IActionResult> B2BPostpaidTariffs()
        {
            var B2BPostpaidTariff = await _context.Tariff
                .Where(t => t.Segment.Title == "B2BPostpaid")
                .ToListAsync();

            var calls = B2BPostpaidTariff
                .Where(t => t.CallSms.Trim().StartsWith("المكالمات"))
                .ToList();

            var sms = B2BPostpaidTariff
                .Where(t => t.CallSms.StartsWith("رسالة") || t.CallSms.StartsWith("الرسائل"))
                .ToList();

            var cug = B2BPostpaidTariff
               .Where(t => t.CallSms.StartsWith("CUG") || t.CallSms.StartsWith("cug"))
               .ToList();

            var vm = new TariffViewModel
            {
                Call = calls,
                Sms = sms
            };

            return View(vm);
        }
    }
}
