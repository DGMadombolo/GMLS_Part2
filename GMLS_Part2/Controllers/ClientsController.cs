using Microsoft.AspNetCore.Mvc;
using GMLS_Part2.Models;
using GMLS_Part2.Services;

namespace GMLS_Part2.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ApiService _apiService;

        public ClientsController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // =====================================================
        // INDEX + SEARCH/FILTER
        // =====================================================

        public async Task<IActionResult> Index(
            string? searchTerm,
            string? region)
        {
            var clients = (await _apiService
                .GetClientsAsync())
                .AsQueryable();

            // =============================================
            // SEARCH CLIENT NAME
            // =============================================

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                clients = clients.Where(c =>
                    c.Name.Contains(searchTerm));
            }

            // =============================================
            // FILTER REGION
            // =============================================

            if (!string.IsNullOrWhiteSpace(region))
            {
                clients = clients.Where(c =>
                    c.Region.Contains(region));
            }

            return View(clients.ToList());
        }

        // =====================================================
        // DETAILS
        // =====================================================

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var client =
                await _apiService.GetClientAsync(id.Value);

            if (client == null)
                return NotFound();

            return View(client);
        }

        // =====================================================
        // CREATE GET
        // =====================================================

        public IActionResult Create()
        {
            return View();
        }

        // =====================================================
        // CREATE POST
        // =====================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Name,ContactDetails,Region")]
            Client client)
        {
            if (ModelState.IsValid)
            {
                await _apiService.CreateClientAsync(client);

                return RedirectToAction(nameof(Index));
            }

            return View(client);
        }

        // =====================================================
        // EDIT GET
        // =====================================================

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var client =
                await _apiService.GetClientAsync(id.Value);

            if (client == null)
                return NotFound();

            return View(client);
        }

        // =====================================================
        // EDIT POST
        // =====================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Name,ContactDetails,Region")]
            Client client)
        {
            if (id != client.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _apiService.UpdateClientAsync(client);

                return RedirectToAction(nameof(Index));
            }

            return View(client);
        }

        // =====================================================
        // DELETE GET
        // =====================================================

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var client =
                await _apiService.GetClientAsync(id.Value);

            if (client == null)
                return NotFound();

            return View(client);
        }

        // =====================================================
        // DELETE POST
        // =====================================================

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(
            int id)
        {
            await _apiService.DeleteClientAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}