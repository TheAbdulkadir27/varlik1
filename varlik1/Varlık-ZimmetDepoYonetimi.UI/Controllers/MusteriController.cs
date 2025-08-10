using Microsoft.AspNetCore.Mvc;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    public class MusteriController : Controller
    {
        private readonly IMusteriService _musteriService;

        public MusteriController(IMusteriService musteriService)
        {
            _musteriService = musteriService;
        }

        public async Task<IActionResult> Index()
        {
            var musteriler = await _musteriService.GetAllMusteriAsync();
            return View(musteriler);
        }

        public async Task<IActionResult> Details(int id)
        {
            var musteri = await _musteriService.GetMusteriByIdAsync(id);
            if (musteri == null)
            {
                return NotFound();
            }
            return View(musteri);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Musteri musteri)
        {
            if (ModelState.IsValid)
            {
                await _musteriService.AddMusteriAsync(musteri);
                return RedirectToAction(nameof(Index));
            }
            return View(musteri);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var musteri = await _musteriService.GetMusteriByIdAsync(id);
            if (musteri == null)
            {
                return NotFound();
            }
            return View(musteri);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Musteri musteri)
        {
            if (id != musteri.MusteriId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _musteriService.UpdateMusteriAsync(musteri);
                return RedirectToAction(nameof(Index));
            }
            return View(musteri);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var musteri = await _musteriService.GetMusteriByIdAsync(id);
            if (musteri == null)
            {
                return NotFound();
            }
            return View(musteri);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musteri = await _musteriService.GetMusteriByIdAsync(id);
            if (musteri == null)
            {
                return NotFound();
            }

            await _musteriService.DeleteMusteriAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
