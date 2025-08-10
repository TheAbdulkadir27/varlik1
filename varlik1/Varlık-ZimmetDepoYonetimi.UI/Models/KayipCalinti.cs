namespace Varlık_ZimmetDepoYonetimi.UI.Models;

public partial class KayipCalinti
{
    public int KayipCalintiId { get; set; }

    public int ZimmetId { get; set; }

    public int? UrunId { get; set; }

    public string? Aciklama { get; set; }

    public DateTime Tarih { get; set; }

    public bool KayipMi { get; set; } = true;  // Varsayılan olarak true (kayıp)

    public bool? AktifMi { get; set; }

    public virtual Urun? Urun { get; set; }

    public virtual Zimmet Zimmet { get; set; } = null!;
}
