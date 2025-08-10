using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Models;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize]
    public class ModelController : Controller
    {
        private readonly IModelService _modelService;
        private readonly ILogger<ModelController> _logger;

        public ModelController(IModelService modelService, ILogger<ModelController> logger)
        {
            _modelService = modelService;
            _logger = logger;
        }
        [Authorize(Policy = Permissions.Modeller.Goruntuleme)]
        public async Task<IActionResult> Index()
        {
            var modeller = await _modelService.GetAllModelsAsync(); //Bütün modelleri getir
            return View(modeller);
        }

        [Authorize(Policy = Permissions.Modeller.Ekleme)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Modeller.Ekleme)]
        public async Task<IActionResult> Create(Model model)
        {
            if (ModelState.IsValid)
            {
                await _modelService.AddModelAsync(model);
                TempData["Success"] = "Model başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Policy = Permissions.Modeller.Duzenleme)]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _modelService.GetModelByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Modeller.Duzenleme)]
        public async Task<IActionResult> Edit(Model model)
        {
            if (ModelState.IsValid)
            {
                await _modelService.UpdateModelAsync(model);
                TempData["Success"] = "Model başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Policy = Permissions.Modeller.Silme)]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _modelService.GetModelByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Modeller.Silme)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _modelService.DeleteModelAsync(id);
            TempData["message"] = message;
            return RedirectToAction(nameof(Index));
        }
    }
}