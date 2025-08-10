namespace Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels
{
    public class StokRaporViewModel //Stok raporlarını tutan sınıf
    {
        public StokRaporViewModel()
        {
            DusukStokluUrunler = new List<Urun>();
            StokDurumu = new Dictionary<string, int>();
            SonStokHareketleri = new List<StokHareket>();
        }


        public int ToplamUrunSayisi { get; set; }
        public List<Urun> DusukStokluUrunler { get; set; }
        public Dictionary<string, int> StokDurumu { get; set; }
        public List<StokHareket> SonStokHareketleri { get; set; }
    }
}