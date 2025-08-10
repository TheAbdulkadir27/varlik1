namespace Varlık_ZimmetDepoYonetimi.UI.Models;

public partial class Model
{
    public int ModelId { get; set; }

    public string? ModelAdi { get; set; }

    public int? UstModelId { get; set; }

    public bool? AktifMi { get; set; }

    public virtual ICollection<Model> InverseUstModel { get; set; } = new List<Model>();

    public virtual ICollection<Urun> Uruns { get; set; } = new List<Urun>();

    public virtual Model? UstModel { get; set; }
}
