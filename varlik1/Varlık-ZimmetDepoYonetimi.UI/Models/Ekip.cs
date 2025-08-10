namespace Varlık_ZimmetDepoYonetimi.UI.Models;

public partial class Ekip
{
    public int EkipId { get; set; }

    public string? EkipAdi { get; set; }

    public bool? AktifMi { get; set; }

    public int? SirketId { get; set; }

    public virtual Sirket? Sirket { get; set; }

    public virtual ICollection<Calisan> Calisans { get; set; } = new List<Calisan>();

    public virtual ICollection<SirketEkip> SirketEkips { get; set; } = new List<SirketEkip>();
}
