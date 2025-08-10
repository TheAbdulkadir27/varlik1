namespace Varlık_ZimmetDepoYonetimi.UI.Models;

public partial class Urun
{
    public int UrunId { get; set; }
    public string? Number { get; set; }
    public int? ModelId { get; set; }

    public bool? GarantiliMi { get; set; }

    public double? UrunMaliyeti { get; set; }

    public string? Aciklama { get; set; }

    public bool? BarkodluMu { get; set; }

    public double? UrunGuncelFiyat { get; set; }
    public bool? AktifMi { get; set; }
    public int StokMiktari { get; set; }
    public int? UnitMeasureId { get; set; }
    public UnitMeasure? UnitMeasure { get; set; }
    public int? ProductGroupId { get; set; }
    public ProductGroup? ProductGroup { get; set; }
    public virtual ICollection<DepoUrun> DepoUruns { get; set; } = new List<DepoUrun>();
    public virtual ICollection<KayipCalinti> KayipCalintis { get; set; } = new List<KayipCalinti>();

    public virtual Model? Model { get; set; }

    public virtual ICollection<UrunBarkod> UrunBarkods { get; set; } = new List<UrunBarkod>();

    public virtual ICollection<UrunDetay> UrunDetays { get; set; } = new List<UrunDetay>();

    public virtual ICollection<UrunStatu> UrunStatus { get; set; } = new List<UrunStatu>();

    public virtual ICollection<Zimmet> Zimmets { get; set; } = new List<Zimmet>();
    public virtual ICollection<ProductGroup> UrunGrubu { get; set; } = new List<ProductGroup>();

    public virtual ICollection<UnitMeasure> UnitMeasures { get; set; } = new List<UnitMeasure>();
}
