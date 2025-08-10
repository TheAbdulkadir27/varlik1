using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Repositories;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Implementations;
using Varlık_ZimmetDepoYonetimi.UI.Repositories.Interfaces;
using Varlık_ZimmetDepoYonetimi.UI.Services.Implementations;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using static Permissions;
namespace Varlık_ZimmetDepoYonetimi.UI;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);



        builder.Services.AddDbContext<VarlikZimmetEnginContext>(options =>
        {
            options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 27)));
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.EnableSensitiveDataLogging();
        });


        builder.WebHost.UseUrls(builder.Configuration["applicationUrl"]!.ToString());
        // Identity servislerini ekleyelim
        builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            // Şifre politikasını basitleştirelim
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 4;

            // Giriş ayarları
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = false;

            // Kullanıcı ayarları
            options.User.RequireUniqueEmail = false;
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        })
        .AddEntityFrameworkStores<VarlikZimmetEnginContext>()
        .AddDefaultTokenProviders();

        // Cookie ayarlarını güncelleyelim
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Account/Logout";
            options.AccessDeniedPath = "/Account/AccessDenied";
            options.Cookie.Name = "VarlikZimmet";
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            options.SlidingExpiration = true;
            // Geliştirme ortamında HTTPS zorunluluğunu kaldıralım
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        });





        // Repositories
        builder.Services.AddScoped<IUrunRepository, UrunRepository>();
        builder.Services.AddScoped<ICalisanRepository, CalisanRepository>();
        builder.Services.AddScoped<IDepoRepository, DepoRepository>();
        builder.Services.AddScoped<IZimmetRepository, ZimmetRepository>();
        builder.Services.AddScoped<ISirketRepository, SirketRepository>();
        builder.Services.AddScoped<IEkipRepository, EkipRepository>();
        builder.Services.AddScoped<IRolRepository, RolRepository>();
        builder.Services.AddScoped<IMusteriRepository, MusteriRepository>();
        builder.Services.AddScoped<IKayipCalintiRepository, KayipCalintiRepository>();
        builder.Services.AddScoped<IStatuRepository, StatuRepository>();
        builder.Services.AddScoped<IModelRepository, ModelRepository>();
        builder.Services.AddScoped<ISayfaRepository, SayfaRepository>();
        builder.Services.AddScoped<IYetkiRepository, YetkiRepository>();
        builder.Services.AddScoped<IRaporRepository, RaporRepository>();
        builder.Services.AddScoped<IStokHareketRepository, StokHareketRepository>();
        builder.Services.AddScoped<ICustomerGroupRepository, CustomerGroupRepository>();
        builder.Services.AddScoped<ICustomerCategoryRepository, CustomerCategoryRepository>();
        builder.Services.AddScoped<ICustomerContactRepository, CustomerContactRepository>();
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        builder.Services.AddScoped<ITaxRepository, TaxRepository>();
        builder.Services.AddScoped<IUnitMeasureRepository, UnitMeasureRepository>();
        builder.Services.AddScoped<IProductGroupRepository, ProductGroupRepository>();
        builder.Services.AddScoped<ISalesOrderRepository, SalesOrderRepository>();
        builder.Services.AddScoped<ISalesOrderItemRepository, SalesOrderitemRepository>();
        builder.Services.AddScoped<IVendorGroupRepository, VendorGroupRepository>();
        builder.Services.AddScoped<IVendorCategoryRepository, VendorCategoryRepository>();
        builder.Services.AddScoped<IVendorRepository, VendorRepository>();
        builder.Services.AddScoped<IVendorContactRepository, VendorContactRepository>();
        builder.Services.AddScoped<IPurchaseRepository, PurchaseOrderRepository>();
        builder.Services.AddScoped<IPurchaseOrderitemRepository, PurchaseOrderitemRepository>();
        builder.Services.AddScoped<IDeliveryOrderRepository, DeliveryOrderRepository>();
        builder.Services.AddScoped<IDeliveryOrderitemRepository, DeliveryOrderitemRepository>();

        // Services
        builder.Services.AddScoped<IUrunService, UrunService>();
        builder.Services.AddScoped<ICalisanService, CalisanService>();
        builder.Services.AddScoped<IDepoService, DepoService>();
        builder.Services.AddScoped<IZimmetService, ZimmetService>();
        builder.Services.AddScoped<ISirketService, SirketService>();
        builder.Services.AddScoped<IEkipService, EkipService>();
        builder.Services.AddScoped<IRolService, RolService>();
        builder.Services.AddScoped<IMusteriService, MusteriService>();
        builder.Services.AddScoped<IKayipCalintiService, KayipCalintiService>();
        builder.Services.AddScoped<IStatuService, StatuService>();
        builder.Services.AddScoped<IModelService, ModelService>();
        builder.Services.AddScoped<ISayfaService, SayfaService>();
        builder.Services.AddScoped<IYetkiService, YetkiService>();
        builder.Services.AddScoped<IRaporService, RaporService>();
        builder.Services.AddScoped<IStokHareketService, StokHareketService>();
        builder.Services.AddScoped<ICustomerGroupService, CustomerGroupService>();
        builder.Services.AddScoped<ICustomerCategoryService, CustomerCategoryService>();
        builder.Services.AddScoped<ICustomerContactService, CustomerContactService>();
        builder.Services.AddScoped<ICustomerService, CustomerService>();
        builder.Services.AddScoped<ITaxService, TaxService>();
        builder.Services.AddScoped<IUnitMeasureService, UnitMeasureService>();
        builder.Services.AddScoped<IProductGroupService, ProductGroupService>();
        builder.Services.AddScoped<ISalesOrderService, SalesOrderService>();
        builder.Services.AddScoped<ISalesOrderItemService, SalesOrderitemService>();
        builder.Services.AddScoped<IVendorGroupService, VendorGroupService>();
        builder.Services.AddScoped<IVendorCategoryService, VendorCategoryService>();
        builder.Services.AddScoped<IVendorService, VendorService>();
        builder.Services.AddScoped<IVendorContactService, VendorContactService>();
        builder.Services.AddScoped<IPurchaseService, PurchaseService>();
        builder.Services.AddScoped<IPurchaseOrderitemService, PurchaseOrderitemService>();
        builder.Services.AddScoped<IDeliveryOrderService, DeliveryOrderService>();
        builder.Services.AddScoped<IDeliveryOrderitemService, DeliveryOrderitemService>();


        // Session servisini ekleyelim
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum süresi
            options.Cookie.HttpOnly = true; // Güvenlik için sadece HTTP üzerinden erişilebilir
            options.Cookie.IsEssential = true; // GDPR ve diğer uyumluluklar için gerekli
        });

        var hostEnv = builder.Environment;
        builder.Host.UseWindowsService();
        if (hostEnv.IsDevelopment())
        {
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }
        else
        {
            builder.Services.AddControllersWithViews();
        }


        // MVC ve Authentication Middleware ekleyelim
        builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
        builder.Services.AddRazorPages();

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAdmin", policy =>
                policy.RequireRole(UserRoles.Admin));




            // Ürün yetkileri
            options.AddPolicy(Permissions.Urunler.Goruntuleme, policy =>
                policy.RequireAssertion(context => context.User.IsInRole(UserRoles.Admin)
                || context.User.IsInRole(UserRoles.DepoSorumlusu)
                || context.User.IsInRole(UserRoles.Calisan)
                || context.User.HasClaim("Permission", Permissions.Urunler.Goruntuleme)
                || context.User.HasClaim("Permission", Permissions.Urunler.Duzenleme)
                || context.User.HasClaim("Permission", Permissions.Urunler.Ekleme)
                || context.User.HasClaim("Permission", Permissions.Urunler.Silme)
                ));

            options.AddPolicy(Permissions.Urunler.Ekleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.IsInRole(UserRoles.DepoSorumlusu)
                || x.User.HasClaim("Permission", Permissions.Urunler.Ekleme)));


            options.AddPolicy(Permissions.Urunler.Duzenleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.IsInRole(UserRoles.DepoSorumlusu)
                || x.User.HasClaim("Permission", Permissions.Urunler.Duzenleme)));

            options.AddPolicy(Permissions.Urunler.Silme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.HasClaim("Permission", Permissions.Urunler.Silme)));


            //modeller
            options.AddPolicy(Permissions.Modeller.Goruntuleme, policy =>
               policy.RequireAssertion(context => context.User.IsInRole(UserRoles.Admin)
               || context.User.IsInRole(UserRoles.DepoSorumlusu)
               || context.User.IsInRole(UserRoles.Calisan)
               || context.User.HasClaim("Permission", Permissions.Modeller.Goruntuleme)
               || context.User.HasClaim("Permission", Permissions.Modeller.Ekleme)
               || context.User.HasClaim("Permission", Permissions.Modeller.Silme)
               || context.User.HasClaim("Permission", Permissions.Modeller.Duzenleme)
               ));

            options.AddPolicy(Permissions.Modeller.Ekleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.IsInRole(UserRoles.DepoSorumlusu)
                || x.User.HasClaim("Permission", Permissions.Modeller.Ekleme)));


            options.AddPolicy(Permissions.Modeller.Duzenleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.IsInRole(UserRoles.DepoSorumlusu)
                || x.User.HasClaim("Permission", Permissions.Modeller.Duzenleme)));

            options.AddPolicy(Permissions.Modeller.Silme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.HasClaim("Permission", Permissions.Modeller.Silme)));


            //statu
            options.AddPolicy(Permissions.Statuler.Goruntuleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.HasClaim("Permission", Permissions.Statuler.Goruntuleme)));





            // Zimmet yetkileri
            options.AddPolicy(Permissions.Zimmetler.Goruntuleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.IsInRole(UserRoles.ZimmetSorumlusu)
                || x.User.IsInRole(UserRoles.Calisan)
                || x.User.HasClaim("Permission", Permissions.Zimmetler.Goruntuleme)
                || x.User.HasClaim("Permission", Permissions.Zimmetler.Düzenleme)
                || x.User.HasClaim("Permission", Permissions.Zimmetler.IptalEtme)
                || x.User.HasClaim("Permission", Permissions.Zimmetler.Atama)
                || x.User.HasClaim("Permission", Permissions.Zimmetler.Silme)
                ));

            options.AddPolicy(Permissions.Zimmetler.Atama, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.IsInRole(UserRoles.ZimmetSorumlusu)
                || x.User.HasClaim("Permission", Permissions.Zimmetler.Atama)));

            options.AddPolicy(Permissions.Zimmetler.IptalEtme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.IsInRole(UserRoles.ZimmetSorumlusu)
                || x.User.HasClaim("Permission", Permissions.Zimmetler.IptalEtme)));


            options.AddPolicy(Permissions.Zimmetler.Düzenleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.IsInRole(UserRoles.ZimmetSorumlusu)
                || x.User.HasClaim("Permission", Permissions.Zimmetler.Düzenleme)));


            options.AddPolicy(Permissions.Zimmetler.Silme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.IsInRole(UserRoles.ZimmetSorumlusu)
                || x.User.HasClaim("Permission", Permissions.Zimmetler.Silme)));

            // Depo yetkileri
            options.AddPolicy(Permissions.Depolar.Goruntuleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.IsInRole(UserRoles.DepoSorumlusu)
                || x.User.HasClaim("Permission", Permissions.Depolar.Goruntuleme)
                || x.User.HasClaim("Permission", Permissions.Depolar.Ekleme)
                || x.User.HasClaim("Permission", Permissions.Depolar.Silme)
                || x.User.HasClaim("Permission", Permissions.Depolar.Duzenleme)
                ));

            options.AddPolicy(Permissions.Depolar.Ekleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin) || x.User.HasClaim("Permission", Permissions.Depolar.Ekleme)));

            options.AddPolicy(Permissions.Depolar.Duzenleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin) || x.User.HasClaim("Permission", Permissions.Depolar.Duzenleme)));

            options.AddPolicy(Permissions.Depolar.Silme, policy =>
               policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin) || x.User.HasClaim("Permission", Permissions.Depolar.Silme)));


            // Ekip yetkileri
            options.AddPolicy(Permissions.Ekipler.Goruntuleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin) || x.User.IsInRole(UserRoles.DepoSorumlusu)
                || x.User.HasClaim("Permission", Permissions.Ekipler.Goruntuleme)
                || x.User.HasClaim("Permission", Permissions.Ekipler.Ekleme)
                || x.User.HasClaim("Permission", Permissions.Ekipler.Silme)
                || x.User.HasClaim("Permission", Permissions.Ekipler.Duzenleme)
                ));

            options.AddPolicy(Permissions.Ekipler.Ekleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin) ||
                x.User.HasClaim("Permission", Permissions.Ekipler.Ekleme)));

            options.AddPolicy(Permissions.Ekipler.Duzenleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin) || x.User.HasClaim("Permission", Permissions.Ekipler.Duzenleme)));

            options.AddPolicy(Permissions.Ekipler.Silme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin) || x.User.HasClaim("Permission", Permissions.Ekipler.Silme)));

            // Çalışan yetkileri
            options.AddPolicy(Permissions.Calisanlar.Goruntuleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.IsInRole(UserRoles.DepoSorumlusu)
                || x.User.IsInRole(UserRoles.ZimmetSorumlusu)
                || x.User.HasClaim("Permission", Permissions.Calisanlar.Goruntuleme)
                || x.User.HasClaim("Permission", Permissions.Calisanlar.Ekleme)
                || x.User.HasClaim("Permission", Permissions.Calisanlar.Silme)
                || x.User.HasClaim("Permission", Permissions.Calisanlar.Duzenleme)
                ));

            options.AddPolicy(Permissions.Calisanlar.Ekleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin) || x.User.HasClaim("Permission", Permissions.Calisanlar.Ekleme)));

            options.AddPolicy(Permissions.Calisanlar.Duzenleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin) || x.User.HasClaim("Permission", Permissions.Calisanlar.Duzenleme)));

            options.AddPolicy(Permissions.Calisanlar.Silme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin) || x.User.HasClaim("Permission", Permissions.Calisanlar.Silme)));


            // Rapor yetkileri
            options.AddPolicy(Permissions.Raporlar.StokRaporu, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin) || x.User.IsInRole(UserRoles.DepoSorumlusu) ||
                x.User.HasClaim("Permission", Permissions.Raporlar.StokRaporu)));

            options.AddPolicy(Permissions.Raporlar.CalisanRaporu, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin) ||
                x.User.IsInRole(UserRoles.DepoSorumlusu) ||
                x.User.HasClaim("Permission", Permissions.Raporlar.CalisanRaporu)));

            options.AddPolicy(Permissions.Raporlar.ZimmetRaporu, policy =>
                policy.RequireAssertion(context => context.User.IsInRole(UserRoles.Admin)
                || context.User.IsInRole(UserRoles.ZimmetSorumlusu)
                || context.User.HasClaim("Permission", Permissions.Raporlar.ZimmetRaporu)));


            // Kayıp/Çalıntı yetkileri
            options.AddPolicy(Permissions.KayıpCalıntı.Goruntuleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin) ||
                x.User.IsInRole(UserRoles.ZimmetSorumlusu)
                || x.User.HasClaim("Permission", Permissions.KayıpCalıntı.Goruntuleme)
                || x.User.HasClaim("Permission", Permissions.KayıpCalıntı.Ekleme)
                || x.User.HasClaim("Permission", Permissions.KayıpCalıntı.Silme)
                ));

            options.AddPolicy(Permissions.KayıpCalıntı.Ekleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin) ||
                x.User.IsInRole(UserRoles.ZimmetSorumlusu) ||
                x.User.HasClaim("Permission", Permissions.KayıpCalıntı.Ekleme)));

            options.AddPolicy(Permissions.KayıpCalıntı.Silme, policy =>
               policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin) || x.User.IsInRole(UserRoles.ZimmetSorumlusu)
               || x.User.HasClaim("Permission", Permissions.KayıpCalıntı.Silme) || x.User.HasClaim("Permission", Permissions.KayıpCalıntı.Goruntuleme)));

            //sirketler
            options.AddPolicy(Permissions.Sirketler.Goruntuleme, policy =>
            policy.RequireAssertion(context =>
            context.User.IsInRole(UserRoles.Admin) ||
            context.User.HasClaim("Permission", Permissions.Sirketler.Goruntuleme)
            || context.User.HasClaim("Permission", Permissions.Sirketler.Ekleme)
            || context.User.HasClaim("Permission", Permissions.Sirketler.Silme)
            || context.User.HasClaim("Permission", Permissions.Sirketler.Duzenleme)
            ));

            options.AddPolicy(Permissions.Sirketler.Ekleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin) || x.User.HasClaim("Permission", Permissions.Sirketler.Ekleme)));

            options.AddPolicy(Permissions.Sirketler.Duzenleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin) || x.User.HasClaim("Permission", Permissions.Sirketler.Duzenleme)));

            options.AddPolicy(Permissions.Sirketler.Silme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin) || x.User.HasClaim("Permission", Permissions.Sirketler.Silme)));

            //Müşteri Grubu
            options.AddPolicy(Permissions.CustomerGroup.Goruntuleme, policy =>
               policy.RequireAssertion(context => context.User.IsInRole(UserRoles.Admin)
               || context.User.IsInRole(UserRoles.DepoSorumlusu)
               || context.User.IsInRole(UserRoles.Calisan)
               || context.User.HasClaim("Permission", Permissions.CustomerGroup.Goruntuleme)
               || context.User.HasClaim("Permission", Permissions.CustomerGroup.Ekleme)
               || context.User.HasClaim("Permission", Permissions.CustomerGroup.Silme)
               || context.User.HasClaim("Permission", Permissions.CustomerGroup.Duzenleme)
               ));

            options.AddPolicy(Permissions.CustomerGroup.Ekleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.IsInRole(UserRoles.DepoSorumlusu)
                || x.User.HasClaim("Permission", Permissions.CustomerGroup.Ekleme)));


            options.AddPolicy(Permissions.CustomerGroup.Duzenleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.IsInRole(UserRoles.DepoSorumlusu)
                || x.User.HasClaim("Permission", Permissions.CustomerGroup.Duzenleme)));

            options.AddPolicy(Permissions.CustomerGroup.Silme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.HasClaim("Permission", Permissions.CustomerGroup.Silme)));


            //Müşteri Kategorisi

            options.AddPolicy(Permissions.CustomerCategory.Goruntuleme, policy =>
              policy.RequireAssertion(context => context.User.IsInRole(UserRoles.Admin)
              || context.User.IsInRole(UserRoles.DepoSorumlusu)
              || context.User.IsInRole(UserRoles.Calisan)
              || context.User.HasClaim("Permission", Permissions.CustomerCategory.Goruntuleme)
              || context.User.HasClaim("Permission", Permissions.CustomerCategory.Ekleme)
              || context.User.HasClaim("Permission", Permissions.CustomerCategory.Silme)
              || context.User.HasClaim("Permission", Permissions.CustomerCategory.Duzenleme)
              ));

            options.AddPolicy(Permissions.CustomerCategory.Ekleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.IsInRole(UserRoles.DepoSorumlusu)
                || x.User.HasClaim("Permission", Permissions.CustomerCategory.Ekleme)));


            options.AddPolicy(Permissions.CustomerCategory.Duzenleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.IsInRole(UserRoles.DepoSorumlusu)
                || x.User.HasClaim("Permission", Permissions.CustomerCategory.Duzenleme)));

            options.AddPolicy(Permissions.CustomerCategory.Silme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.HasClaim("Permission", Permissions.CustomerCategory.Silme)));

            //Müşteri İletişim

            options.AddPolicy(Permissions.CustomerContact.Goruntuleme, policy =>
             policy.RequireAssertion(context => context.User.IsInRole(UserRoles.Admin)
             || context.User.IsInRole(UserRoles.DepoSorumlusu)
             || context.User.IsInRole(UserRoles.Calisan)
             || context.User.HasClaim("Permission", Permissions.CustomerContact.Goruntuleme)
             || context.User.HasClaim("Permission", Permissions.CustomerContact.Ekleme)
             || context.User.HasClaim("Permission", Permissions.CustomerContact.Silme)
             || context.User.HasClaim("Permission", Permissions.CustomerContact.Duzenleme)
             ));

            options.AddPolicy(Permissions.CustomerContact.Ekleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.IsInRole(UserRoles.DepoSorumlusu)
                || x.User.HasClaim("Permission", Permissions.CustomerContact.Ekleme)));


            options.AddPolicy(Permissions.CustomerContact.Duzenleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.IsInRole(UserRoles.DepoSorumlusu)
                || x.User.HasClaim("Permission", Permissions.CustomerContact.Duzenleme)));

            options.AddPolicy(Permissions.CustomerContact.Silme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.HasClaim("Permission", Permissions.CustomerContact.Silme)));

            //Vergi

            options.AddPolicy(Permissions.Tax.Goruntuleme, policy =>
             policy.RequireAssertion(context => context.User.IsInRole(UserRoles.Admin)
             || context.User.IsInRole(UserRoles.DepoSorumlusu)
             || context.User.IsInRole(UserRoles.Calisan)
             || context.User.HasClaim("Permission", Permissions.Tax.Goruntuleme)
             || context.User.HasClaim("Permission", Permissions.Tax.Ekleme)
             || context.User.HasClaim("Permission", Permissions.Tax.Silme)
             || context.User.HasClaim("Permission", Permissions.Tax.Duzenleme)
             ));

            options.AddPolicy(Permissions.Tax.Ekleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.IsInRole(UserRoles.DepoSorumlusu)
                || x.User.HasClaim("Permission", Permissions.Tax.Ekleme)));


            options.AddPolicy(Permissions.Tax.Duzenleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.IsInRole(UserRoles.DepoSorumlusu)
                || x.User.HasClaim("Permission", Permissions.Tax.Duzenleme)));

            options.AddPolicy(Permissions.Tax.Silme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.HasClaim("Permission", Permissions.Tax.Silme)));





            //Müşteri
            options.AddPolicy(Permissions.Customer.Goruntuleme, policy =>
             policy.RequireAssertion(context => context.User.IsInRole(UserRoles.Admin)
             || context.User.IsInRole(UserRoles.DepoSorumlusu)
             || context.User.IsInRole(UserRoles.Calisan)
             || context.User.HasClaim("Permission", Permissions.Customer.Goruntuleme)
             || context.User.HasClaim("Permission", Permissions.Customer.Ekleme)
             || context.User.HasClaim("Permission", Permissions.Customer.Silme)
             || context.User.HasClaim("Permission", Permissions.Customer.Duzenleme)
             ));

            options.AddPolicy(Permissions.Customer.Ekleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.IsInRole(UserRoles.DepoSorumlusu)
                || x.User.HasClaim("Permission", Permissions.Customer.Ekleme)));


            options.AddPolicy(Permissions.Customer.Duzenleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.IsInRole(UserRoles.DepoSorumlusu)
                || x.User.HasClaim("Permission", Permissions.Customer.Duzenleme)));

            options.AddPolicy(Permissions.Customer.Silme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.HasClaim("Permission", Permissions.Customer.Silme)));

            //ÜnitMeasure
            options.AddPolicy(Permissions.UnitMeasure.Goruntuleme, policy =>
             policy.RequireAssertion(context => context.User.IsInRole(UserRoles.Admin)
             || context.User.IsInRole(UserRoles.DepoSorumlusu)
             || context.User.IsInRole(UserRoles.Calisan)
             || context.User.HasClaim("Permission", Permissions.UnitMeasure.Goruntuleme)
             || context.User.HasClaim("Permission", Permissions.UnitMeasure.Ekleme)
             || context.User.HasClaim("Permission", Permissions.UnitMeasure.Silme)
             || context.User.HasClaim("Permission", Permissions.UnitMeasure.Duzenleme)
             ));

            options.AddPolicy(Permissions.UnitMeasure.Ekleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.IsInRole(UserRoles.DepoSorumlusu)
                || x.User.HasClaim("Permission", Permissions.UnitMeasure.Ekleme)));


            options.AddPolicy(Permissions.UnitMeasure.Duzenleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.IsInRole(UserRoles.DepoSorumlusu)
                || x.User.HasClaim("Permission", Permissions.UnitMeasure.Duzenleme)));

            options.AddPolicy(Permissions.UnitMeasure.Silme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.HasClaim("Permission", Permissions.UnitMeasure.Silme)));

            //ProductGroup
            options.AddPolicy(Permissions.ProductGroup.Goruntuleme, policy =>
             policy.RequireAssertion(context => context.User.IsInRole(UserRoles.Admin)
             || context.User.IsInRole(UserRoles.DepoSorumlusu)
             || context.User.IsInRole(UserRoles.Calisan)
             || context.User.HasClaim("Permission", Permissions.ProductGroup.Goruntuleme)
             || context.User.HasClaim("Permission", Permissions.ProductGroup.Ekleme)
             || context.User.HasClaim("Permission", Permissions.ProductGroup.Silme)
             || context.User.HasClaim("Permission", Permissions.ProductGroup.Duzenleme)
             ));

            options.AddPolicy(Permissions.ProductGroup.Ekleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.IsInRole(UserRoles.DepoSorumlusu)
                || x.User.HasClaim("Permission", Permissions.ProductGroup.Ekleme)));


            options.AddPolicy(Permissions.ProductGroup.Duzenleme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.IsInRole(UserRoles.DepoSorumlusu)
                || x.User.HasClaim("Permission", Permissions.ProductGroup.Duzenleme)));

            options.AddPolicy(Permissions.ProductGroup.Silme, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin)
                || x.User.HasClaim("Permission", Permissions.ProductGroup.Silme)));





            //backup restore
            options.AddPolicy(Permissions.BackupRestore.Yedek, policy =>
                policy.RequireAssertion(x => x.User.IsInRole(UserRoles.Admin) || x.User.HasClaim("Permission", Permissions.BackupRestore.Yedek)));



        });

        builder.Services.AddMemoryCache();
        builder.Services.AddScoped<ICacheService, MemoryCacheService>();
        builder.Services.AddHttpContextAccessor();
        var app = builder.Build();

        // Middleware'leri ekleyelim
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        app.UseStatusCodePagesWithRedirects("/Account/ErrorPage");
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        // Bu sıralama önemli
        app.UseSession(); // Session
        app.UseAuthentication(); // Önce Authentication. Önce kimlik doğrulama yapılır.
        app.UseAuthorization();  // Sonra Authorization. Kimlik doğrulama sonrası yetkilendirme yapılır.

        // Default yönlendirme
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        // Rol oluşturma ve atama
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                // Rolleri oluştur
                var roles = new[] { UserRoles.Admin, UserRoles.User, UserRoles.DepoSorumlusu, UserRoles.ZimmetSorumlusu, UserRoles.Calisan };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                // Admin kullanıcısını oluştur
                var adminUser = await userManager.FindByNameAsync("Admin");
                if (adminUser == null)
                {
                    var admin = new IdentityUser
                    {
                        UserName = "Admin",
                        Email = "admin@example.com",
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(admin, "Admin123!"); // Daha güçlü bir şifre
                    if (result.Succeeded)
                    {
                        // Admin rolünü ata
                        await userManager.AddToRoleAsync(admin, UserRoles.Admin);
                    }
                }
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Seed data oluşturulurken bir hata oluştu.");
            }
        }

        // Veritabanını güncelle
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<VarlikZimmetEnginContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Veritabanı migration sırasında bir hata oluştu.");
            }
        }

        // Veritabanı seed işlemi
        //using (var scope = app.Services.CreateScope())
        //{
        //    var services = scope.ServiceProvider;
        //    try
        //    {
        //        var context = services.GetRequiredService<VarlikZimmetEnginContext>();

        //        // Model verileri
        //        if (!context.Model.Any())
        //        {
        //            var models = new List<Model>
        //            {
        //                new Model { ModelAdi = "Laptop", AktifMi = true },
        //                new Model { ModelAdi = "Masaüstü PC", AktifMi = true },
        //                new Model { ModelAdi = "Monitör", AktifMi = true },
        //                new Model { ModelAdi = "Yazıcı", AktifMi = true }
        //            };
        //            context.Model.AddRange(models);
        //        }

        //        // Ürün verileri
        //        if (!context.Uruns.Any())
        //        {
        //            var urunler = new List<Urun>
        //            {
        //                new Urun
        //                {
        //                    ModelId = 1,
        //                    GarantiliMi = true,
        //                    UrunMaliyeti = 15000,
        //                    Aciklama = "Dell Latitude",
        //                    BarkodluMu = true,
        //                    AktifMi = true
        //                },
        //                new Urun
        //                {
        //                    ModelId = 2,
        //                    GarantiliMi = true,
        //                    UrunMaliyeti = 20000,
        //                    Aciklama = "HP Workstation",
        //                    BarkodluMu = true,
        //                    AktifMi = true
        //                }
        //            };
        //            context.Uruns.AddRange(urunler);
        //        }

        //        // Depo verileri
        //        if (!context.Depo.Any())
        //        {
        //            var depolar = new List<Depo>
        //            {
        //                new Depo { DepoAdi = "Ana Depo", AktifMi = true },
        //                new Depo { DepoAdi = "Yedek Depo", AktifMi = true }
        //            };
        //            context.Depo.AddRange(depolar);
        //        }

        //        await context.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        var logger = services.GetRequiredService<ILogger<Program>>();
        //        logger.LogError(ex, "Örnek veri oluşturulurken bir hata oluştu.");
        //    }
        //}
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });
        app.Run();
    }
}
