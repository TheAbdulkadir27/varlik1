namespace Varlık_ZimmetDepoYonetimi.UI.Models;

public partial class Rol
{
    public int RolId { get; set; }

    public string? RolAdi { get; set; }

    public bool? AktifMi { get; set; }

    public virtual ICollection<Calisan> Calisans { get; set; } = new List<Calisan>();

    public virtual ICollection<SayfaYetki> SayfaYetkis { get; set; } = new List<SayfaYetki>();
}
