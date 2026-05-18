using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GMLS_Part2.Data;
using GMLS_Part2.Models;
using GMLS_Part2.Services;

namespace GMLS_Part2.Controllers
{
    public class ServiceRequestsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly CurrencyService _currencyService;

        public ServiceRequestsController(
            AppDbContext context,
            CurrencyService currencyService)
        {
            _context = context;
            _currencyService = currencyService;
        }

        // =====================================================
        // INDEX + SEARCH/FILTER
        // =====================================================

        public async Task<IActionResult> Index(
            RequestStatus? status,
            string? searchTerm)
        {
            var serviceRequests = _context.ServiceRequests
                .Include(s => s.Contract)
                .AsQueryable();

            // =============================================
            // FILTER BY STATUS
            // =============================================

            if (status.HasValue)
            {
                serviceRequests = serviceRequests
                    .Where(s =>
                        s.Status == status.Value);
            }

            // =============================================
            // SEARCH DESCRIPTION
            // =============================================

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                serviceRequests = serviceRequests
                    .Where(s =>
                        s.Description.Contains(searchTerm));
            }

            return View(await serviceRequests.ToListAsync());
        }

        // =====================================================
        // DETAILS
        // =====================================================

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var serviceRequest = await _context.ServiceRequests
                .Include(s => s.Contract)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (serviceRequest == null)
                return NotFound();

            return View(serviceRequest);
        }

        // =====================================================
        // CREATE GET
        // =====================================================

        public IActionResult Create()
        {
            ViewData["ContractId"] = new SelectList(
                _context.Contracts,
                "Id",
                "Id");

            return View();
        }

        // =====================================================
        // CREATE POST
        // =====================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            ServiceRequest serviceRequest)
        {
            // =============================================
            // CHECK CONTRACT
            // =============================================

            var contract = await _context.Contracts
                .FirstOrDefaultAsync(c =>
                    c.Id == serviceRequest.ContractId);

            if (contract == null)
            {
                ModelState.AddModelError(
                    "",
                    "Selected contract does not exist.");
            }
            else if (contract.Status == ContractStatus.Expired)
            {
                ModelState.AddModelError(
                    "",
                    "Cannot create requests for expired contracts.");
            }
            else if (contract.Status == ContractStatus.OnHold)
            {
                ModelState.AddModelError(
                    "",
                    "Cannot create requests for contracts on hold.");
            }

            // =============================================
            // SAVE REQUEST
            // =============================================

            if (ModelState.IsValid)
            {
                // AUTO CONVERT USD → ZAR

                serviceRequest.CostZAR =
                    await _currencyService
                        .ConvertUsdToZarAsync(
                            serviceRequest.CostUSD);

                _context.ServiceRequests.Add(serviceRequest);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["ContractId"] = new SelectList(
                _context.Contracts,
                "Id",
                "Id",
                serviceRequest.ContractId);

            return View(serviceRequest);
        }

        // =====================================================
        // EDIT GET
        // =====================================================

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var serviceRequest =
                await _context.ServiceRequests.FindAsync(id);

            if (serviceRequest == null)
                return NotFound();

            ViewData["ContractId"] = new SelectList(
                _context.Contracts,
                "Id",
                "Id",
                serviceRequest.ContractId);

            return View(serviceRequest);
        }

        // =====================================================
        // EDIT POST
        // =====================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            ServiceRequest serviceRequest)
        {
            if (id != serviceRequest.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // AUTO RECALCULATE USD → ZAR

                    serviceRequest.CostZAR =
                        await _currencyService
                            .ConvertUsdToZarAsync(
                                serviceRequest.CostUSD);

                    _context.Update(serviceRequest);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceRequestExists(serviceRequest.Id))
                        return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["ContractId"] = new SelectList(
                _context.Contracts,
                "Id",
                "Id",
                serviceRequest.ContractId);

            return View(serviceRequest);
        }

        // =====================================================
        // DELETE GET
        // =====================================================

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var serviceRequest = await _context.ServiceRequests
                .Include(s => s.Contract)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (serviceRequest == null)
                return NotFound();

            return View(serviceRequest);
        }

        // =====================================================
        // DELETE POST
        // =====================================================

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceRequest =
                await _context.ServiceRequests.FindAsync(id);

            if (serviceRequest != null)
            {
                _context.ServiceRequests.Remove(serviceRequest);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // =====================================================
        // EXISTS
        // =====================================================

        private bool ServiceRequestExists(int id)
        {
            return _context.ServiceRequests
                .Any(e => e.Id == id);
        }
    }
}