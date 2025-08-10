namespace Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels
{
    public class KullaniciYetkiViewModel
    {
        public string? UserId { get; set; }

        public List<YetkiCheckbox> Yetkiler { get; set; } = new();
    }
    public class YetkiCheckbox
    {
        public string Value { get; set; }     // Yetki ID'si (claim değeri)
        public string Aciklama { get; set; }  // Yetki açıklaması
        public bool Secili { get; set; }      // Checkbox işaretli mi?
    }
}
