using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class YetkiController : Controller
    {
        private readonly IYetkiService _yetkiService;

        public YetkiController(IYetkiService yetkiService)
        {
            _yetkiService = yetkiService;
        }

        public IActionResult Index()
        {
            var permissions = new List<YetkiListeViewModel>();

            // Permissions sınıfındaki tüm yetkileri reflection ile alıyoruz
            var permissionTypes = typeof(Permissions).GetNestedTypes(BindingFlags.Public | BindingFlags.Static);
            foreach (var type in permissionTypes)
            {
                var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                foreach (var field in fields)
                {
                    var value = field.GetValue(null)?.ToString() ?? "";
                    var categoryName = type.Name;
                    var permissionName = field.Name;

                    permissions.Add(new YetkiListeViewModel
                    {
                        Kategori = categoryName,
                        YetkiAdi = permissionName,
                        TamAdi = value
                    });
                }
            }

            // Kategoriye ve yetki adına göre sıralama
            permissions = permissions.OrderBy(p => p.Kategori).ThenBy(p => p.YetkiAdi).ToList();
            return View(permissions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult YetkiEkle(YetkiEkleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Yetki eklenirken bir hata oluştu.";
                return RedirectToAction(nameof(Index));
            }
            // Burada yeni yetki ekleme işlemleri yapılacak
            TempData["Success"] = "Yetki başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult YetkiSil(string yetkiAdi)
        {
            if (string.IsNullOrEmpty(yetkiAdi))
            {
                TempData["Error"] = "Yetki adı boş olamaz.";
                return RedirectToAction(nameof(Index));
            }

            // Burada yetki silme işlemleri yapılacak
            TempData["Success"] = "Yetki başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}