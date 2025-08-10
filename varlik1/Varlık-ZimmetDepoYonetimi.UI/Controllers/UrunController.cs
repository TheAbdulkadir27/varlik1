using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Services.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Filters;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Models.Enums;
using Varlık_ZimmetDepoYonetimi.UI.Models.Global;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace VarlikZimmetWebApp.Controllers
{
    [Authorize]
    public class UrunController : Controller
    {
        private readonly VarlikZimmetEnginContext _db;
        private readonly ILogger<UrunController> _logger;
        private readonly IUrunService _urunService;
        private readonly IModelService _modelService;
        private readonly IDepoService _depoService;
        private readonly IStatuService _statuService;
        private readonly IZimmetService _zimmetservice;
        private readonly IUnitMeasureService _unitmeasureservice;
        private readonly IProductGroupService _productGroupService;

        public UrunController(VarlikZimmetEnginContext db, ILogger<UrunController> logger, IUrunService urunService, IModelService modelService, IDepoService depoService, IStatuService statuService, IZimmetService zimmetservice, IUnitMeasureService unitmeasureservice, IProductGroupService productGroupService)
        {
            _db = db;
            _logger = logger;
            _urunService = urunService;
            _modelService = modelService;
            _depoService = depoService;
            _statuService = statuService;
            _zimmetservice = zimmetservice;
            _unitmeasureservice = unitmeasureservice;
            _productGroupService = productGroupService;
        }

        [Authorize(Policy = Permissions.Urunler.Goruntuleme)]
        public async Task<IActionResult> Index()
        {
            var urunler = (await _urunService.GetAllUrunsAsync()).Where(x => !x.Zimmets.Any(z => z.UrunId == x.UrunId && z.AktifMi == true)).ToList();
            var viewModel = urunler.Select(u => new UrunViewModel
            {
                UrunId = u.UrunId,
                ModelId = u.ModelId,
                Number = u.Number,
                Model = u.Model,
                GarantiliMi = u.GarantiliMi,
                UrunMaliyeti = u.UrunMaliyeti,
                Aciklama = u.Aciklama,
                BarkodluMu = u.BarkodluMu,
                UrunGuncelFiyat = u.UrunGuncelFiyat,
                AktifMi = u.AktifMi ?? false,
                StokMiktari = u.StokMiktari,
                StatuId = u.UrunStatus.OrderByDescending(us => us.Tarih).FirstOrDefault()?.StatuId,
                Statu = u.UrunStatus.OrderByDescending(us => us.Tarih).FirstOrDefault()?.Statu,
                ProductGroupId = u.UrunGrubu.OrderByDescending(x => x.Name).FirstOrDefault()?.ID,
                UnitMeasureId = u.UnitMeasures.OrderByDescending(x => x.Name).FirstOrDefault()?.ID
            }).ToList();
            return View(viewModel);
        }
        [UrunKontrolFiltresi]
        public async Task<IActionResult> Details(int id)
        {
            var urun = await _urunService.GetUrunByIdAsync(id);
            if (urun == null)
            {
                return NotFound();
            }
            return View(urun);
        }

        [Authorize(Policy = Permissions.Urunler.Ekleme)]
        public async Task<IActionResult> Create()
        {

            var model = new UrunViewModel
            {
                Modeller = (await _modelService.GetModelSelectListAsync()).ToList(),
                Depolar = await GetDepolarSelectList(),
                Statuler = await GetStatulerSelectList(),
                ÖlçüBirimi = await GetUnitMeasureSelectList(),
                ÜrünGrubu = await GetUrunGrubuSelectList(),
                Number = "[Otomatik]"
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Urunler.Ekleme)]
        public async Task<IActionResult> Create(UrunViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Modeller = (await _modelService.GetModelSelectListAsync()).ToList();
                viewModel.Depolar = await GetDepolarSelectList();
                viewModel.Statuler = await GetStatulerSelectList();
                viewModel.ÜrünGrubu = await GetUrunGrubuSelectList();
                viewModel.ÖlçüBirimi = await GetUnitMeasureSelectList();
                return View(viewModel);
            }
            if(viewModel.Number is null || viewModel.Number == string.Empty)
            {
                var autonumber = _urunService.GetAllUrunsAsync().Result.Count() + 1;
                viewModel.Number = GenerateNumber.GenerateProductNumber(autonumber, NumberModels.Product);
            }

            string acıklama = string.Empty;
            if (viewModel.Aciklama?.Length > 50)
            {
                acıklama = viewModel.Aciklama.Substring(0, 50);
            }
            else
            {
                acıklama = viewModel.Aciklama!;
            }

            var urun = new Urun
            {
                ModelId = viewModel.ModelId,
                GarantiliMi = viewModel.GarantiliMi,
                UrunMaliyeti = viewModel.UrunMaliyeti,
                Aciklama = acıklama,
                Number = viewModel.Number,
                BarkodluMu = viewModel.BarkodluMu,
                UrunGuncelFiyat = viewModel.UrunGuncelFiyat,
                AktifMi = viewModel.AktifMi,
                StokMiktari = viewModel.StokMiktari,
                UnitMeasureId = viewModel.UnitMeasureId,
                ProductGroupId = viewModel.ProductGroupId,
                UrunStatus = new List<UrunStatu>(),
                DepoUruns = new List<DepoUrun>(),
                UrunGrubu = new List<ProductGroup>(),
                UnitMeasures = new List<UnitMeasure>(),
            };

            if (viewModel.DepoId.HasValue)
            {
                urun.DepoUruns.Add(new DepoUrun
                {
                    DepoId = viewModel.DepoId.Value,
                    Miktar = (short)viewModel.StokMiktari
                });
            }

            if (viewModel.StatuId.HasValue)
            {
                urun.UrunStatus.Add(new UrunStatu
                {
                    StatuId = viewModel.StatuId.Value,
                    Tarih = DateTime.Now,
                    AktifMi = true
                });
            }
            else if (!viewModel.StatuId.HasValue)
            {
                urun.UrunStatus.Add(new UrunStatu
                {
                    StatuId = 4,
                    Tarih = DateTime.Now,
                    AktifMi = true
                });
            }

            await _urunService.AddUrunAsync(urun);
            return RedirectToAction(nameof(Index));
        }

        private async Task<List<SelectListItem>> GetModellerSelectList()
        {
            var modeller = await _modelService.GetAllModelsAsync();
            return modeller.Select(m => new SelectListItem
            {
                Value = m.ModelId.ToString(),
                Text = m.ModelAdi
            }).ToList();
        }

        private async Task<List<SelectListItem>> GetDepolarSelectList()
        {
            var depolar = await _depoService.GetAllDeposAsync();
            return depolar.Select(d => new SelectListItem
            {
                Value = d.DepoId.ToString(),
                Text = d.DepoAdi
            }).ToList();
        }
        private async Task<List<SelectListItem>> GetUnitMeasureSelectList()
        {
            var unitMeasures = await _unitmeasureservice.GetAllAsync();
            return unitMeasures.Select(d => new SelectListItem
            {
                Value = d.ID.ToString(),
                Text = d.Name
            }).ToList();
        }

        private async Task<List<SelectListItem>> GetUrunGrubuSelectList()
        {
            var productGroup = await _productGroupService.GetAllAsync();
            return productGroup.Select(d => new SelectListItem
            {
                Value = d.ID.ToString(),
                Text = d.Name
            }).ToList();
        }

        private async Task<List<SelectListItem>> GetStatulerSelectList()
        {
            var statuler = await _statuService.GetStatuSelectListAsync();
            return statuler.ToList();
        }

        [Authorize(Policy = Permissions.Urunler.Duzenleme)]
        public async Task<IActionResult> Edit(int id)
        {
            var urun = await _urunService.GetUrunByIdAsync(id);
            if (urun == null)
            {
                return NotFound();
            }

            var viewModel = new UrunViewModel
            {
                UrunId = urun.UrunId,
                ModelId = urun.ModelId,
                GarantiliMi = urun.GarantiliMi,
                UrunMaliyeti = urun.UrunMaliyeti,
                Aciklama = urun.Aciklama,
                BarkodluMu = urun.BarkodluMu,
                Number = urun.Number,
                UrunGuncelFiyat = urun.UrunGuncelFiyat,
                AktifMi = urun.AktifMi ?? true,
                StatuId = urun.UrunStatus.OrderByDescending(us => us.Tarih).FirstOrDefault()?.StatuId,
                ProductGroupId = urun.UrunGrubu.OrderByDescending(x => x.Name).FirstOrDefault()?.ID,
                UnitMeasureId = urun.UnitMeasures.OrderByDescending(x => x.Name).FirstOrDefault()?.ID,
                StokMiktari = urun.StokMiktari,
                DepoId = urun.DepoUruns.FirstOrDefault()?.DepoId,
                Modeller = await GetModellerSelectList(),
                Depolar = await GetDepolarSelectList(),
                Statuler = await GetStatulerSelectList(),
                ÖlçüBirimi = await GetUnitMeasureSelectList(),
                ÜrünGrubu = await GetUrunGrubuSelectList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Urunler.Duzenleme)]
        public async Task<IActionResult> Edit(int id, UrunViewModel viewModel)
        {
            if (id != viewModel.UrunId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var urun = await _urunService.GetUrunByIdAsync(id);
                if (urun == null)
                {
                    return NotFound();
                }

                urun.ModelId = viewModel.ModelId;
                urun.GarantiliMi = viewModel.GarantiliMi;
                urun.Number = viewModel.Number;
                urun.UrunMaliyeti = viewModel.UrunMaliyeti;
                urun.Aciklama = viewModel.Aciklama;
                urun.BarkodluMu = viewModel.BarkodluMu;
                urun.UrunGuncelFiyat = viewModel.UrunGuncelFiyat;
                urun.AktifMi = viewModel.AktifMi;
                urun.StokMiktari = viewModel.StokMiktari;
                urun.UnitMeasureId = viewModel.UnitMeasureId;
                urun.ProductGroupId = viewModel.ProductGroupId;
                var sonStatu = urun.UrunStatus.OrderByDescending(us => us.Tarih).FirstOrDefault();
                if (viewModel.DepoId.HasValue)
                {

                    using (var context = new VarlikZimmetEnginContext())
                    {
                        try
                        {
                            var urunum = context.DepoUruns.Where(x => x.UrunId == viewModel.UrunId).FirstOrDefault();
                            if (urunum != null)
                            {
                                urunum.DepoId = viewModel.DepoId;
                                urunum.Miktar = (short)viewModel.StokMiktari;
                                context.DepoUruns.Update(urunum);
                            }
                            else
                            {
                                urun.DepoUruns.Clear();
                                context.DepoUruns.Add(new DepoUrun()
                                {
                                    UrunId = viewModel.UrunId,
                                    DepoId = viewModel.DepoId,
                                    Miktar = (short)viewModel.StokMiktari
                                });
                            }
                            await context.SaveChangesAsync();
                        }
                        catch (Exception)
                        {

                        }
                    }
                }


                if (viewModel.StatuId > 0 && (sonStatu == null || sonStatu.StatuId != viewModel.StatuId))
                {
                    using (var context = new VarlikZimmetEnginContext())
                    {
                        try
                        {
                            var urunum = context.UrunStatus.Where(x => x.UrunId == urun.UrunId).FirstOrDefault();
                            if (urunum != null)
                            {
                                urunum.StatuId = viewModel.StatuId.Value;
                                urunum.Tarih = DateTime.Now;
                                context.UrunStatus.Update(urunum);
                                context.SaveChanges();
                            }
                            else
                            {
                                context.UrunStatus.Add(new UrunStatu()
                                {
                                    UrunId = viewModel.UrunId,
                                    StatuId = viewModel.StatuId.Value,
                                    Tarih = DateTime.Now
                                });
                                context.SaveChanges();
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
                await _urunService.UpdateUrunAsync(urun);

                return RedirectToAction(nameof(Index));
            }

            viewModel.Modeller = await GetModellerSelectList();
            viewModel.Depolar = await GetDepolarSelectList();
            viewModel.Statuler = await GetStatulerSelectList();
            viewModel.ÜrünGrubu = await GetUrunGrubuSelectList();
            viewModel.ÖlçüBirimi = await GetUnitMeasureSelectList();
            return View(viewModel);
        }

        [UrunKontrolFiltresi]
        [Authorize(Policy = Permissions.Urunler.Silme)]
        public async Task<IActionResult> Delete(int id)
        {
            var urun = await _urunService.GetUrunByIdAsync(id);
            if (urun == null)
            {
                return NotFound();
            }

            var model = new UrunViewModel
            {
                UrunId = urun.UrunId,
                ModelId = urun.ModelId,
                GarantiliMi = urun.GarantiliMi ?? false,
                UrunMaliyeti = urun.UrunMaliyeti ?? 0,
                Aciklama = urun.Aciklama,
                BarkodluMu = urun.BarkodluMu ?? false,
                UrunGuncelFiyat = urun.UrunGuncelFiyat ?? 0,
                AktifMi = urun.AktifMi ?? false,
                Model = urun.Model,
                Number = urun.Number,
                ProductGroupId = urun.ProductGroupId,
                UnitMeasureId = urun.UnitMeasureId
            };

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Permissions.Urunler.Silme)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _urunService.DeleteUrunAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
