namespace Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            StokDurumu = new Dictionary<string, int>();
        }

        public int ToplamCalisanSayisi { get; set; }
        public int ToplamDepoSayisi { get; set; }
        public int AktifZimmetSayisi { get; set; }
        public IEnumerable<Urun> DusukStokluUrunSayisi { get; set; }
        public int ToplamUrunSayisi { get; set; }
        public Dictionary<string, int> StokDurumu { get; set; }
        public IEnumerable<Zimmet> SonZimmetler { get; set; }
        public decimal calintiUrunOrani { get; set; }
    }
}