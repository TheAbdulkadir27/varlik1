namespace Varlık_ZimmetDepoYonetimi.UI.Models;

public partial class Zimmet
{
    public int ZimmetId { get; set; }

    public string ZimmetNumarasi { get; set; } = null!;

    public int? UrunId { get; set; }

    public int? AtananCalisanId { get; set; }

    public int? AtayanCalisanId { get; set; }

    public DateTime? ZimmetBaslangicTarihi { get; set; }

    public DateTime? ZimmetBitisTarihi { get; set; }

    public string? Aciklama { get; set; }

    public bool? AktifMi { get; set; }

    public DateTime ZimmetTarihi { get; set; }

    public DateTime? IadeTarihi { get; set; }

    public virtual Calisan? AtananCalisan { get; set; }

    public virtual Calisan? AtayanCalisan { get; set; }

    public virtual ICollection<MusteriZimmet> MusteriZimmets { get; set; } = new List<MusteriZimmet>();

    public virtual Urun? Urun { get; set; }
}
