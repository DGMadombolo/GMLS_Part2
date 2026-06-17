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
        private readonly ApiService _apiService;


    public ServiceRequestsController(
        AppDbContext context,
        CurrencyService currencyService,
        ApiService apiService)
        {
            _context = context;
            _currencyService = currencyService;
            _apiService = apiService;
        }

        // =====================================================
        // INDEX + SEARCH/FILTER
        // =====================================================

        public async Task<IActionResult> Index(
            RequestStatus? status,
            string? searchTerm)
        {
            var serviceRequests = (await _apiService
                .GetServiceRequestsAsync())
                .AsQueryable();

            if (status.HasValue)
            {
                serviceRequests = serviceRequests
                    .Where(s =>
                        s.Status == status.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                serviceRequests = serviceRequests
                    .Where(s =>
                        s.Description.Contains(searchTerm));
            }

            return View(serviceRequests.ToList());
        }

        // =====================================================
        // DETAILS
        // =====================================================

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var serviceRequest =
                await _apiService.GetServiceRequestAsync(id.Value);

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

            if (ModelState.IsValid)
            {
                serviceRequest.CostZAR =
                    await _currencyService
                        .ConvertUsdToZarAsync(
                            serviceRequest.CostUSD);

                await _apiService
                    .CreateServiceRequestAsync(serviceRequest);

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
                await _apiService.GetServiceRequestAsync(id.Value);

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
                    serviceRequest.CostZAR =
                        await _currencyService
                            .ConvertUsdToZarAsync(
                                serviceRequest.CostUSD);

                    await _apiService
                        .UpdateServiceRequestAsync(
                            serviceRequest);
                }
                catch (Exception)
                {
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

            var serviceRequest =
                await _apiService.GetServiceRequestAsync(id.Value);

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
            await _apiService
                .DeleteServiceRequestAsync(id);

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
