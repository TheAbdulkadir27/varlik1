namespace Varlık_ZimmetDepoYonetimi.UI.Models;

public partial class Sirket
{
    public Sirket()
    {
        Ekipler = new HashSet<Ekip>();
    }

    public int SirketId { get; set; }

    public string? SirketAdi { get; set; }

    public bool? AktifMi { get; set; }

    public virtual ICollection<Depo> Depos { get; set; } = new List<Depo>();

    public virtual ICollection<SirketEkip> SirketEkips { get; set; } = new List<SirketEkip>();

    public virtual ICollection<Ekip> Ekipler { get; set; }

    public virtual ICollection<Calisan> Calisans { get; set; } = new List<Calisan>();
}
