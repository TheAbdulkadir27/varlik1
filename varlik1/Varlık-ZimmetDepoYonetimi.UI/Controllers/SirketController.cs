using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize]
    public class SirketController : Controller
    {
        private readonly ISirketService _sirketService;
        private readonly ILogger<SirketController> _logger;
        private readonly VarlikZimmetEnginContext _context;

        public SirketController(ISirketService sirketService, ILogger<SirketController> logger, VarlikZimmetEnginContext context)
        {
            _sirketService = sirketService;
            _logger = logger;
            _context = context;
        }

        [Authorize(Policy = Permissions.Sirketler.Goruntuleme)]
        public async Task<IActionResult> Index()
        {
            var sirketler = await _context.Sirkets
                .Include(s => s.SirketEkips)
                .Include(s => s.Calisans)
                .ToListAsync();
            return View(sirketler);
        }

        [Authorize(Policy = Permissions.Sirketler.Ekleme)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Sirketler.Ekleme)]
        public async Task<IActionResult> Create(Sirket sirket)
        {
            if (ModelState.IsValid)
            {
                await _sirketService.AddAsync(sirket);
                TempData["Success"] = "Şirket başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(sirket);
        }

        [Authorize(Policy = Permissions.Sirketler.Duzenleme)]
        public async Task<IActionResult> Edit(int id)
        {
            var sirket = await _sirketService.GetByIdAsync(id);
            if (sirket == null)
            {
                return NotFound();
            }
            return View(sirket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Sirketler.Duzenleme)]
        public async Task<IActionResult> Edit(Sirket sirket)
        {
            if (ModelState.IsValid)
            {
                await _sirketService.UpdateAsync(sirket);
                TempData["Success"] = "Şirket başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(sirket);
        }
        [Authorize(Policy = Permissions.Sirketler.Silme)]
        public async Task<IActionResult> Delete(int id)
        {
            var message = await _sirketService.DeleteAsync(id);
            TempData["message"] = message;
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = Permissions.Sirketler.Goruntuleme)]
        public async Task<IActionResult> Details(int id)
        {
            var sirket = await _context.Sirkets
                .Include(s => s.SirketEkips)
                    .ThenInclude(se => se.Ekip)
                .Include(s => s.Calisans)
                    .ThenInclude(c => c.Ekip)
                .FirstOrDefaultAsync(s => s.SirketId == id);

            if (sirket == null)
            {
                return NotFound();
            }

            return View(sirket);
        }
    }
}

