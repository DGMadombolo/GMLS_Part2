using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GMLS_Part2.Data;
using GMLS_Part2.Models;

namespace GMLS_Part2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(
            ILogger<HomeController> logger,
            AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // =====================================================
        // DASHBOARD
        // =====================================================

        public async Task<IActionResult> Index()
        {
            // =============================================
            // DASHBOARD COUNTS
            // =============================================

            ViewBag.TotalClients =
                await _context.Clients.CountAsync();

            ViewBag.TotalContracts =
                await _context.Contracts.CountAsync();

            ViewBag.ActiveContracts =
                await _context.Contracts
                    .CountAsync(c =>
                        c.Status ==
                        ContractStatus.Active);

            ViewBag.TotalRequests =
                await _context.ServiceRequests
                    .CountAsync();

            ViewBag.CompletedRequests =
                await _context.ServiceRequests
                    .CountAsync(r =>
                        r.Status ==
                        RequestStatus.Completed);

            return View();
        }

        // =====================================================
        // PRIVACY
        // =====================================================

        public IActionResult Privacy()
        {
            return View();
        }

        // =====================================================
        // ERROR
        // =====================================================

        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]

        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId =
                        Activity.Current?.Id
                        ?? HttpContext.TraceIdentifier
                });
        }
    }
}