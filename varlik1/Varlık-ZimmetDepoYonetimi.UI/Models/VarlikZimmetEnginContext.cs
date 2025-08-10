using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Varlık_ZimmetDepoYonetimi.UI.Models;

public partial class VarlikZimmetEnginContext : IdentityDbContext
{
    public VarlikZimmetEnginContext()
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    public VarlikZimmetEnginContext(DbContextOptions<VarlikZimmetEnginContext> options)
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    public virtual DbSet<DeliveryOrderitem> DeliveryOrderitems { get; set; }
    public virtual DbSet<DeliveryOrder> DeliveryOrders { get; set; }
    public virtual DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    public virtual DbSet<VendorContact> VendorContacts { get; set; }
    public virtual DbSet<Vendor> Vendor { get; set; }
    public virtual DbSet<VendorGroup> VendorGroups { get; set; }
    public virtual DbSet<VendorCategory> VendorCategories { get; set; }
    public virtual DbSet<CustomerGroup> CustomerGroup { get; set; }
    public virtual DbSet<CustomerCategory> CustomerCategory { get; set; }
    public virtual DbSet<Customer> Customer { get; set; }
    public virtual DbSet<CustomerContact> CustomerContacts { get; set; }

    public virtual DbSet<Tax> Tax { get; set; }

    public virtual DbSet<UnitMeasure> UnitMeasures { get; set; }
    public virtual DbSet<ProductGroup> ProductGroup { get; set; }
    public virtual DbSet<SalesOrder> SalesOrder { get; set; }
    public virtual DbSet<SalesOrderItem> SalesOrderItem { get; set; }
    public virtual DbSet<Calisan> Calisan { get; set; }

    public virtual DbSet<Depo> Depo { get; set; }

    public virtual DbSet<DepoUrun> DepoUruns { get; set; }

    public virtual DbSet<Ekip> Ekips { get; set; }

    public virtual DbSet<KayipCalinti> KayipCalintis { get; set; }

    public virtual DbSet<Model> Model { get; set; }

    public virtual DbSet<Musteri> Musteris { get; set; }

    public virtual DbSet<MusteriZimmet> MusteriZimmets { get; set; }

    public virtual DbSet<MusteriZimmetIptalIade> MusteriZimmetIptalIades { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Sayfa> Sayfas { get; set; }

    public virtual DbSet<SayfaYetki> SayfaYetkis { get; set; }

    public virtual DbSet<Sirket> Sirkets { get; set; }

    public virtual DbSet<SirketEkip> SirketEkips { get; set; }

    public virtual DbSet<Statu> Status { get; set; }

    public virtual DbSet<Urun> Uruns { get; set; }

    public virtual DbSet<UrunBarkod> UrunBarkods { get; set; }

    public virtual DbSet<UrunDetay> UrunDetays { get; set; }

    public virtual DbSet<UrunStatu> UrunStatus { get; set; }

    public virtual DbSet<VwKayipCalintiListesi> VwKayipCalintiListesis { get; set; }

    public virtual DbSet<Yetki> Yetkis { get; set; }

    public virtual DbSet<Zimmet> Zimmet { get; set; }

    public DbSet<Urun> Urun { get; set; }

    public DbSet<YetkiTalep> YetkiTalepleri { get; set; }
    public DbSet<StokHareket> StokHareket { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseMySql(configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 27)));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Calisan>(entity =>
        {
            entity.ToTable("Calisan");

            entity.Property(e => e.CalisanId).HasColumnName("CalisanID");
            entity.Property(e => e.AboneNo)
                .HasMaxLength(12);

            entity.Property(e => e.AdSoyad).HasMaxLength(50);
            entity.Property(e => e.AktifMi).HasDefaultValue(true);
            entity.Property(e => e.EkipId).HasColumnName("EkipID");
            entity.Property(e => e.KullaniciAdi).HasMaxLength(50);
            entity.Property(e => e.Mail).HasMaxLength(50);
            entity.Property(e => e.RolId).HasColumnName("RolID");
            entity.Property(e => e.Sifre).HasMaxLength(255);
            entity.Property(e => e.Telefon).HasMaxLength(50);

            entity.HasOne(d => d.Ekip).WithMany(p => p.Calisans)
                .HasForeignKey(d => d.EkipId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Calisan_Ekip");

            entity.HasOne(d => d.Rol).WithMany(p => p.Calisans)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Calisan_Rol");

            entity.HasMany(c => c.Zimmetler)
                .WithOne(z => z.AtananCalisan)
                .HasForeignKey(z => z.AtananCalisanId)
                 .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Depo>(entity =>
        {
            entity.ToTable("Depo");

            entity.Property(e => e.DepoId).HasColumnName("DepoID");
            entity.Property(e => e.AktifMi).HasDefaultValue(true);
            entity.Property(e => e.DepoAdi).HasMaxLength(50);
            entity.Property(e => e.SirketId).HasColumnName("SirketID");

            entity.HasOne(d => d.Sirket).WithMany(p => p.Depos)
                .HasForeignKey(d => d.SirketId)
                 .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Depo_Sirket");
        });

        modelBuilder.Entity<DepoUrun>(entity =>
        {
            entity.ToTable("DepoUrun");

            entity.Property(e => e.DepoUrunId).HasColumnName("DepoUrunID");
            entity.Property(e => e.AktifMi).HasDefaultValue(true);
            entity.Property(e => e.Birim)
                .HasMaxLength(5);

            entity.Property(e => e.DepoId).HasColumnName("DepoID");
            entity.Property(e => e.UrunId).HasColumnName("UrunID");

            entity.HasOne(d => d.Depo).WithMany(p => p.DepoUruns)
                .HasForeignKey(d => d.DepoId)
                 .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_DepoUrun_Depo");

            entity.HasOne(d => d.Urun).WithMany(p => p.DepoUruns)
                .HasForeignKey(d => d.UrunId)
                 .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_DepoUrun_Urun");
        });

        modelBuilder.Entity<Ekip>(entity =>
        {
            entity.ToTable("Ekip");

            entity.Property(e => e.EkipId).HasColumnName("EkipID");
            entity.Property(e => e.AktifMi).HasDefaultValue(true);
            entity.Property(e => e.EkipAdi).HasMaxLength(50);
            entity.Property(e => e.SirketId).HasColumnName("SirketID");

            entity.HasOne(d => d.Sirket).WithMany(p => p.Ekipler)
                .HasForeignKey(d => d.SirketId)
                 .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Ekip_Sirket");
        });

        modelBuilder.Entity<KayipCalinti>(entity =>
        {
            entity.ToTable("KayipCalinti");

            entity.Property(e => e.KayipCalintiId).HasColumnName("KayipCalintiID");
            entity.Property(e => e.Aciklama).HasMaxLength(50);
            entity.Property(e => e.AktifMi).HasDefaultValue(true);
            entity.Property(e => e.UrunId).HasColumnName("UrunID");

            entity.HasOne(d => d.Urun).WithMany(p => p.KayipCalintis)
                .HasForeignKey(d => d.UrunId)
                 .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_KayipCalinti_Urun");
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.ToTable("Model");

            entity.HasKey(e => e.ModelId);

            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.ModelAdi).HasMaxLength(50);
            entity.Property(e => e.UstModelId).HasColumnName("UstModelID");
            entity.Property(e => e.AktifMi).HasDefaultValue(true);

            entity.HasOne(d => d.UstModel)
                .WithMany(p => p.InverseUstModel)
                .HasForeignKey(d => d.UstModelId)
                 .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Model_Model");
        });

        modelBuilder.Entity<Musteri>(entity =>
        {
            entity.ToTable("Musteri");

            entity.Property(e => e.MusteriId).HasColumnName("MusteriID");
            entity.Property(e => e.AdSoyad).HasMaxLength(50);
            entity.Property(e => e.AktifMi).HasDefaultValue(true);
        });

        modelBuilder.Entity<MusteriZimmet>(entity =>
        {
            entity.HasKey(e => e.MusteriZimmetId).HasName("PK_MusteriZimmet_1");

            entity.ToTable("MusteriZimmet");

            entity.Property(e => e.MusteriZimmetId).HasColumnName("MusteriZimmetID");
            entity.Property(e => e.AktifMi).HasDefaultValue(true);
            entity.Property(e => e.MusteriId).HasColumnName("MusteriID");
            entity.Property(e => e.TuketmeTarihi).HasColumnType("datetime");
            entity.Property(e => e.ZimmetId).HasColumnName("ZimmetID");

            entity.HasOne(d => d.Musteri).WithMany(p => p.MusteriZimmets)
                .HasForeignKey(d => d.MusteriId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_MusteriZimmet_Musteri");

            entity.HasOne(d => d.Zimmet).WithMany(p => p.MusteriZimmets)
                .HasForeignKey(d => d.ZimmetId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_MusteriZimmet_Zimmet");
        });

        modelBuilder.Entity<MusteriZimmetIptalIade>(entity =>
        {
            entity.HasKey(e => e.MusteriZimmetİptalIadeId);

            entity.ToTable("MusteriZimmetIptalIade");

            entity.Property(e => e.MusteriZimmetİptalIadeId).HasColumnName("MusteriZimmetİptalIadeID");
            entity.Property(e => e.Aciklama)
                .HasMaxLength(50);

            entity.Property(e => e.AktifMi).HasDefaultValue(true);
            entity.Property(e => e.MusteriZimmetId).HasColumnName("MusteriZimmetID");
            entity.Property(e => e.Tarih)
                .HasMaxLength(50);

            entity.HasOne(d => d.MusteriZimmet).WithMany(p => p.MusteriZimmetIptalIades)
                .HasForeignKey(d => d.MusteriZimmetId)
                 .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_MusteriZimmetIptalIade_MusteriZimmet");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.ToTable("Rol");

            entity.Property(e => e.RolId).HasColumnName("RolID");
            entity.Property(e => e.AktifMi).HasDefaultValue(true);
            entity.Property(e => e.RolAdi).HasMaxLength(50);
        });

        modelBuilder.Entity<Sayfa>(entity =>
        {
            entity.ToTable("Sayfa");

            entity.Property(e => e.SayfaId).HasColumnName("SayfaID");
            entity.Property(e => e.AktifMi).HasDefaultValue(true);
            entity.Property(e => e.SayfaAd).HasMaxLength(100);
            entity.Property(e => e.SayfaPath).HasMaxLength(50);
        });

        modelBuilder.Entity<SayfaYetki>(entity =>
        {
            entity.HasKey(e => new { e.SayfaId, e.YetkiId, e.RolId });

            entity.ToTable("SayfaYetki");

            entity.Property(e => e.SayfaId).HasColumnName("SayfaID");
            entity.Property(e => e.YetkiId).HasColumnName("YetkiID");
            entity.Property(e => e.RolId).HasColumnName("RolID");
            entity.Property(e => e.AktifMi).HasDefaultValue(true);

            entity.HasOne(d => d.Rol).WithMany(p => p.SayfaYetkis)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_SayfaYetki_Rol");

            entity.HasOne(d => d.Sayfa).WithMany(p => p.SayfaYetkis)
                .HasForeignKey(d => d.SayfaId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_SayfaYetki_Sayfa");

            entity.HasOne(d => d.Yetki).WithMany(p => p.SayfaYetkis)
                .HasForeignKey(d => d.YetkiId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_SayfaYetki_Yetki");
        });

        modelBuilder.Entity<Sirket>(entity =>
        {
            entity.ToTable("Sirket");

            entity.Property(e => e.SirketId).HasColumnName("SirketID");
            entity.Property(e => e.AktifMi).HasDefaultValue(true);
            entity.Property(e => e.SirketAdi).HasMaxLength(50);
        });

        modelBuilder.Entity<SirketEkip>(entity =>
        {

            // SirketEkip tablosunun birincil anahtarını tanımlıyoruz
            modelBuilder.Entity<SirketEkip>()
                .HasKey(e => new { e.SirketId, e.EkipId });

            // Tablo adı
            modelBuilder.Entity<SirketEkip>()
                .ToTable("SirketEkip");

            // SirketId ve EkipId'nin sütun adlarını ayarlıyoruz
            modelBuilder.Entity<SirketEkip>()
                .Property(e => e.SirketId)
                .HasColumnName("SirketID");

            modelBuilder.Entity<SirketEkip>()
                .Property(e => e.EkipId)
                .HasColumnName("EkipID");

            modelBuilder.Entity<SirketEkip>()
                .Property(e => e.AktifMi)
                .HasDefaultValue(true);

            // Sirket ve Ekip ilişkilerini tanımlıyoruz
            modelBuilder.Entity<SirketEkip>()
                .HasOne(d => d.Ekip)
                .WithMany(p => p.SirketEkips)
                .HasForeignKey(d => d.EkipId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_SirketEkip_Ekip");

            modelBuilder.Entity<SirketEkip>()
                .HasOne(d => d.Sirket)
                .WithMany(p => p.SirketEkips)
                .HasForeignKey(d => d.SirketId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_SirketEkip_Sirket");
        });

        modelBuilder.Entity<Statu>(entity =>
        {
            entity.ToTable("Statu");

            entity.Property(e => e.StatuId)
                .ValueGeneratedNever()
                .HasColumnName("StatuID");
            entity.Property(e => e.AktifMi).HasDefaultValue(true);
            entity.Property(e => e.StatuAdi).HasMaxLength(50);
        });

        modelBuilder.Entity<Urun>(entity =>
        {
            entity.ToTable("Urun");

            entity.Property(e => e.UrunId).HasColumnName("UrunID");
            entity.Property(e => e.Aciklama).HasMaxLength(100);
            entity.Property(e => e.AktifMi).HasDefaultValue(true);
            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.UrunGuncelFiyat).HasColumnType("DECIMAL(19, 4)");
            entity.Property(e => e.UrunMaliyeti).HasColumnType("DECIMAL(19, 4)");

            entity.HasOne(d => d.Model).WithMany(p => p.Uruns)
                .HasForeignKey(d => d.ModelId)
                 .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Urun_Model");
        });

        modelBuilder.Entity<UrunBarkod>(entity =>
        {
            entity.ToTable("UrunBarkod");

            entity.Property(e => e.UrunBarkodId).HasColumnName("UrunBarkodID");
            entity.Property(e => e.AktifMi).HasDefaultValue(true);
            entity.Property(e => e.UrunId).HasColumnName("UrunID");

            entity.HasOne(d => d.Urun).WithMany(p => p.UrunBarkods)
                .HasForeignKey(d => d.UrunId)
                 .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_UrunBarkod_Urun");
        });

        modelBuilder.Entity<UrunDetay>(entity =>
        {
            entity.ToTable("UrunDetay", tb => tb.HasTrigger("TRG_KayitEklendi"));

            entity.Property(e => e.UrunDetayId)
                .ValueGeneratedNever()
                .HasColumnName("UrunDetayID");
            entity.Property(e => e.AktifMi).HasDefaultValue(true);
            entity.Property(e => e.UrunId).HasColumnName("UrunID");

            entity.HasOne(d => d.Urun).WithMany(p => p.UrunDetays)
                .HasForeignKey(d => d.UrunId)
                .HasConstraintName("FK_UrunDetay_Urun");
        });

        modelBuilder.Entity<UrunStatu>(entity =>
        {
            entity.HasKey(e => e.UrunStatuId).HasName("PK_UrunStatu_1");

            entity.ToTable("UrunStatu");

            entity.Property(e => e.UrunStatuId).HasColumnName("UrunStatuID");
            entity.Property(e => e.AktifMi).HasDefaultValue(true);
            entity.Property(e => e.StatuId).HasColumnName("StatuID");
            entity.Property(e => e.Tarih).HasColumnType("datetime");
            entity.Property(e => e.UrunId).HasColumnName("UrunID");

            entity.HasOne(d => d.Statu).WithMany(p => p.UrunStatus)
                .HasForeignKey(d => d.StatuId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_UrunStatu_Statu");

            entity.HasOne(d => d.Urun).WithMany(p => p.UrunStatus)
                .HasForeignKey(d => d.UrunId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_UrunStatu_Urun");
        });

        modelBuilder.Entity<VwKayipCalintiListesi>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_KayipCalintiListesi");

            entity.Property(e => e.Aciklama).HasMaxLength(50);
            entity.Property(e => e.KayipCalintiId)
                .ValueGeneratedOnAdd()
                .HasColumnName("KayipCalintiID");
            entity.Property(e => e.UrunId).HasColumnName("UrunID");
        });

        modelBuilder.Entity<Yetki>(entity =>
        {
            entity.ToTable("Yetki");

            entity.Property(e => e.YetkiId).HasColumnName("YetkiID");
            entity.Property(e => e.AktifMi).HasDefaultValue(true);
            entity.Property(e => e.YetkiAdi).HasMaxLength(50);
        });


        modelBuilder.Entity<YetkiTalep>()
       .HasOne(t => t.User)
       .WithMany()
       .HasForeignKey(t => t.UserId);

        modelBuilder.Entity<Zimmet>(entity =>
        {
            entity.ToTable("Zimmet", tb => tb.HasTrigger("TRG_ZimmetVerildi"));

            entity.Property(e => e.ZimmetId).HasColumnName("ZimmetID");
            entity.Property(e => e.Aciklama)
                .HasMaxLength(50);

            entity.Property(e => e.AktifMi).HasDefaultValue(true);
            entity.Property(e => e.AtayanCalisanId).HasColumnName("AtayanCalisanID");
            entity.Property(e => e.UrunId).HasColumnName("UrunID");
            entity.Property(e => e.ZimmetBaslangicTarihi).HasColumnType("datetime");
            entity.Property(e => e.ZimmetBitisTarihi).HasColumnType("datetime");

            entity.HasOne(d => d.AtayanCalisan).WithMany(p => p.ZimmetAtayanCalisans)
                .HasForeignKey(d => d.AtayanCalisanId)
                 .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Zimmet_Calisan1");

            entity.HasOne(d => d.Urun).WithMany(p => p.Zimmets)
                .HasForeignKey(d => d.UrunId)
                 .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Zimmet_Urun");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
