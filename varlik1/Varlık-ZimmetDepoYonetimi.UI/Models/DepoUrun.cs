namespace Varlık_ZimmetDepoYonetimi.UI.Models;

public partial class DepoUrun
{
    public int DepoUrunId { get; set; }

    public int? DepoId { get; set; }

    public int? UrunId { get; set; }

    public short? Miktar { get; set; }

    public string? Birim { get; set; }

    public bool? AktifMi { get; set; }

    public virtual Depo? Depo { get; set; }

    public virtual Urun? Urun { get; set; }
}
