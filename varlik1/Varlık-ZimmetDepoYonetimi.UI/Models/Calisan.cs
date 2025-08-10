namespace Varlık_ZimmetDepoYonetimi.UI.Models;

public partial class Calisan
{
    public Calisan()
    {
        Zimmetler = new HashSet<Zimmet>();
    }

    public int CalisanId { get; set; }

    public string? AdSoyad { get; set; }

    public string? Telefon { get; set; }

    public string? Mail { get; set; }

    public int? RolId { get; set; }

    public int? EkipId { get; set; }

    public string? AboneNo { get; set; }

    public byte[]? PasswordHash { get; set; }

    public byte[]? PasswordSalt { get; set; }

    public bool? AktifMi { get; set; }

    public string? KullaniciAdi { get; set; }

    public string? Sifre { get; set; }

    public int? SirketId { get; set; }

    public virtual Ekip? Ekip { get; set; }

    public virtual Rol? Rol { get; set; }

    public virtual ICollection<Zimmet> ZimmetAtananCalisans { get; set; } = new List<Zimmet>();

    public virtual ICollection<Zimmet> ZimmetAtayanCalisans { get; set; } = new List<Zimmet>();

    public virtual ICollection<Zimmet> Zimmetler { get; set; }

    public virtual Sirket? Sirket { get; set; }
}
