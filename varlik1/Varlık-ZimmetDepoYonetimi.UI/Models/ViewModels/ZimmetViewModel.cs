using Microsoft.AspNetCore.Mvc.Rendering;
using Varlýk_ZimmetDepoYonetimi.UI.Models;

public class ZimmetViewModel
{
    public ZimmetViewModel()
    {
        Urunler = new List<SelectListItem>();
        Calisanlar = new List<SelectListItem>();
        Modeller = new List<SelectListItem>();
    }
    public int ZimmetId { get; set; }
    public int? UrunId { get; set; }
    public int? ModelID { get; set; }
    public int? AtananCalisanId { get; set; }
    public DateTime? ZimmetBaslangicTarihi { get; set; }
    public string? Aciklama { get; set; }
    public List<SelectListItem> Urunler { get; set; }
    public List<SelectListItem> Calisanlar { get; set; }
    public List<SelectListItem> Modeller { get; set; }
}