namespace Varlık_ZimmetDepoYonetimi.UI.Models;

public partial class Yetki
{
    public int YetkiId { get; set; }

    public string? YetkiAdi { get; set; }

    public bool? AktifMi { get; set; }

    public virtual ICollection<SayfaYetki> SayfaYetkis { get; set; } = new List<SayfaYetki>();
}
