using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize]
    public class SayfaController : Controller
    {
        private readonly ISayfaService _sayfaService;

        public SayfaController(ISayfaService sayfaService)
        {
            _sayfaService = sayfaService;
        }

        public async Task<IActionResult> Index()
        {
            var sayfalar = await _sayfaService.GetAllAsync();
            return View(sayfalar);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sayfa sayfa)
        {
            if (ModelState.IsValid)
            {
                await _sayfaService.AddAsync(sayfa);
                return RedirectToAction(nameof(Index));
            }
            return View(sayfa);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var sayfa = await _sayfaService.GetByIdAsync(id);
            if (sayfa == null)
            {
                return NotFound();
            }
            return View(sayfa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Sayfa sayfa)
        {
            if (ModelState.IsValid)
            {
                await _sayfaService.UpdateAsync(sayfa);
                return RedirectToAction(nameof(Index));
            }
            return View(sayfa);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _sayfaService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}