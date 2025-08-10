using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize]
    public class StatuController : Controller
    {
        private readonly IStatuService _statuService;
        private readonly IUrunService _urunService;

        public StatuController(IStatuService statuService, IUrunService urunService)
        {
            _statuService = statuService;
            _urunService = urunService;
        }


        [Authorize(Policy =Permissions.Statuler.Goruntuleme)]
        public async Task<IActionResult> Index()
        {
            var urunlerVeStatuleri = await _statuService.GetUrunStatuListesiAsync();
            return View(urunlerVeStatuleri);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var urun = await _urunService.GetUrunByIdAsync(id);
            if (urun == null)
            {
                return NotFound();
            }

            var sonStatu = urun.UrunStatus.OrderByDescending(x => x.Tarih).FirstOrDefault();

            var model = new UrunStatuViewModel
            {
                UrunId = urun.UrunId,
                UrunAdi = urun.Model?.ModelAdi ?? "Belirtilmemiş",
                StatuId = sonStatu?.StatuId ?? 0,
                StatuAdi = sonStatu?.Statu?.StatuAdi ?? "Belirtilmemiş",
                Açıklama = urun.Aciklama,
                Statuler = (await _statuService.GetStatuSelectListAsync()).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UrunStatuViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Statuler = (await _statuService.GetStatuSelectListAsync()).ToList();
                return View(model);
            }
            var urun = await _urunService.GetUrunByIdAsync(model.UrunId);
            if (urun == null)
            {
                return NotFound();
            }
            using (var context = new VarlikZimmetEnginContext())
            {
                var status = context.UrunStatus.Where(x => x.UrunId == model.UrunId).FirstOrDefault();
                if (status != null)
                {
                    status!.Tarih = DateTime.Now;
                    status.StatuId = model.YeniStatuId;
                    context.UrunStatus.Update(status);
                    context.SaveChanges();
                }
            }
            await _urunService.UpdateUrunAsync(urun);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _statuService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Create()
        {
            var defaultStatuler = new List<Statu>
            {
                new Statu { StatuAdi = "Kullanımda", AktifMi = true },
                new Statu { StatuAdi = "Arızalı", AktifMi = true },
                new Statu { StatuAdi = "Tamirde", AktifMi = true },
                new Statu { StatuAdi = "Kullanım Dışı", AktifMi = true }
            };

            foreach (var statu in defaultStatuler)
            {
                await _statuService.AddAsync(statu);
            }

            TempData["Success"] = "Varsayılan statüler başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }
    }
}