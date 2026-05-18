using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GMLS_Part2.Data;
using GMLS_Part2.Models;

namespace GMLS_Part2.Controllers
{
    public class ClientsController : Controller
    {
        private readonly AppDbContext _context;

        public ClientsController(AppDbContext context)
        {
            _context = context;
        }

        // =====================================================
        // INDEX + SEARCH/FILTER
        // =====================================================

        public async Task<IActionResult> Index(
            string? searchTerm,
            string? region)
        {
            var clients = _context.Clients
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

            return View(await clients.ToListAsync());
        }

        // =====================================================
        // DETAILS
        // =====================================================

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);

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
                _context.Add(client);

                await _context.SaveChangesAsync();

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
                await _context.Clients.FindAsync(id);

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
                try
                {
                    _context.Update(client);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
                        return NotFound();

                    throw;
                }

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

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);

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
            var client =
                await _context.Clients.FindAsync(id);

            if (client != null)
            {
                _context.Clients.Remove(client);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // =====================================================
        // EXISTS
        // =====================================================

        private bool ClientExists(int id)
        {
            return _context.Clients
                .Any(e => e.Id == id);
        }
    }
}