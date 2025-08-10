namespace Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels
{
    public enum StatuTipi
    {
        Kullanımda = 1,
        Arızalı = 2,
        Tamirde = 3,
        KullanimDisi = 4
    }

    public class UrunStatuListesiViewModel
    {
        public int UrunId { get; set; }
        public string UrunAdi { get; set; } = string.Empty;
        public string ModelAdi { get; set; } = string.Empty;
        public int StokMiktari { get; set; }
        public double? UrunMaliyeti { get; set; }
        public int StatuId { get; set; }
        public string StatuAdi { get; set; } = string.Empty;
        public StatuTipi StatuTipi { get; set; }
        public DateTime SonGuncellemeTarihi { get; set; }
        public bool AktifMi { get; set; }
        public string? Açıklama { get; set; }
    }
}