# Varlık ve Zimmet Depo Yönetimi Sistemi

## 📋 Proje Hakkında

Bu proje, şirketlerin varlık yönetimi, zimmet takibi ve depo operasyonlarını yönetmek için geliştirilmiş kapsamlı bir web uygulamasıdır. ASP.NET Core MVC mimarisi kullanılarak geliştirilmiştir.

## 🚀 Özellikler

- **Varlık Yönetimi**
  - Ürün ekleme, düzenleme ve silme
  - Stok takibi ve stok hareketleri izleme
  - Düşük stok uyarıları
  - Ürün kategorileri ve model yönetimi

- **Zimmet İşlemleri**
  - Personele zimmet atama
  - Zimmet geçmişi takibi
  - Zimmet iade işlemleri
  - Kayıp/çalıntı ürün takibi

- **Depo Yönetimi**
  - Çoklu depo desteği
  - Depo bazlı stok takibi
  - Depo transferleri
  - Depo envanteri raporlama

- **Raporlama**
  - Zimmet raporları
  - Stok durumu raporları
  - Departman bazlı zimmet analizi
  - Özelleştirilebilir rapor filtreleri

- **Kullanıcı Yönetimi**
  - Rol tabanlı yetkilendirme
  - Detaylı izin yönetimi
  - Kullanıcı aktivite takibi

## 🛠️ Teknolojiler

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Bootstrap 5
- jQuery
- Chart.js

## ⚙️ Kurulum

1. Projeyi klonlayın:
```bash
git clone [repo-url]
```

2. Veritabanını oluşturun:
```bash
dotnet ef database update
```

3. Bağlantı dizesini düzenleyin:
`appsettings.json` dosyasında veritabanı bağlantı bilgilerinizi güncelleyin.

4. Projeyi çalıştırın:
```bash
dotnet run
```

## 📦 Veritabanı Yapılandırması

Proje ilk çalıştırıldığında aşağıdaki seed verileri otomatik olarak oluşturulur:
- Temel roller ve yetkiler
- Örnek ürün modelleri
- Varsayılan statüler

## 👥 Roller ve Yetkiler

- **Admin**: Tüm sistem yetkilerine sahip
- **Zimmet Yöneticisi**: Zimmet atama ve iade işlemleri
- **Depo Sorumlusu**: Depo ve stok yönetimi
- **Ürün Yöneticisi**: Ürün ve model yönetimi

## 📊 Raporlar

- Zimmet Raporu
  - Aktif zimmetler
  - Departman bazlı zimmet dağılımı
  - Zimmet oranları

- Stok Raporu
  - Düşük stoklu ürünler
  - Stok hareket geçmişi
  - Stok durumu grafikleri

## 🔒 Güvenlik

- Windows Authentication desteği
- Rol tabanlı erişim kontrolü
- Detaylı yetkilendirme sistemi
- Güvenli parola politikaları

## 🤝 Katkıda Bulunma

1. Fork yapın
2. Feature branch oluşturun (`git checkout -b feature/yeniOzellik`)
3. Değişikliklerinizi commit edin (`git commit -m 'Yeni özellik eklendi'`)
4. Branch'inizi push edin (`git push origin feature/yeniOzellik`)
5. Pull Request oluşturun

## 📝 Lisans

Bu proje MIT lisansı altında lisanslanmıştır. Detaylar için [LICENSE](LICENSE) dosyasını inceleyebilirsiniz. 
