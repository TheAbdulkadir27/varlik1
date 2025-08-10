namespace Varlık_ZimmetDepoYonetimi.UI.Models;

public partial class Sayfa
{
    public int SayfaId { get; set; }

    public string? SayfaPath { get; set; }

    public bool? AktifMi { get; set; }

    public string? SayfaAd { get; set; }

    public virtual ICollection<SayfaYetki> SayfaYetkis { get; set; } = new List<SayfaYetki>();
}
