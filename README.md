# 🎫 Destek Kayıt Sistemi

Destek Kayıt Sistemi, kurumların **destek taleplerini yönetebileceği, envanter takibi yapabileceği ve raporlama ekranları üzerinden süreçlerini analiz edebileceği** bir web uygulamasıdır.  

Proje, **Katmanlı Mimari (Layered Architecture)** prensiplerine göre geliştirilmiş olup **ASP.NET Core (MVC)**, **Entity Framework Core** ve **MSSQL Server** üzerine inşa edilmiştir.  
Arayüz geliştirmede **Bootstrap**, **TailwindCSS**, **SweetAlert** ve **Font Awesome** kullanılmıştır.  

---

## 🎯 Genel Bakış
- Kullanıcılar, **ana sayfa** üzerinden destek talebi (ticket) oluşturabilir ve ticket oluşturma sırasında ilgili personele atama yapabilir.  
- Personeller, yalnızca kendilerine yetkilendirilmiş ekranlarda ticket’ları yönetebilir, önceliklendirebilir ve işleme alabilir.  
- **Envanter ekranı** üzerinden cihaz ekleme, listeleme, devretme ve devredilmiş cihazların takibi yapılabilir.  
- **Raporlama ekranında**, destek süreçleriyle ilgili detaylı istatistikler ve grafikler sunulur.  
- **Admin rolü**ne sahip kullanıcılar, sistem ayarlarını yönetebilir; kullanıcı kaydı oluşturabilir, roller ekleyebilir ve rol bazlı yetki atayabilir.  

---

## ✨ Özellikler

### 📌 Ticket Yönetimi
- Ticket oluşturma ve personele atama  
- Ticket tablosu üzerinden mevcut kayıtların listelenmesi  
- Personel ekranında ticket önceliğini değiştirme ve işleme alma  

### 📌 Envanter Yönetimi
- Yeni cihaz ekleme  
- Mevcut cihazları listeleme  
- Cihaz devretme  
- Devredilmiş cihazların takibini yapma  

### 📌 Raporlama
- En çok çözüme ulaşan personellerin grafikleri  
- En çok kayıt açılan konu başlıkları  
- Çözülen / çözülmeyen konu istatistikleri  
- En çok kayıt açan müşteriler  
- Açık, kapalı, bekleyen ticket sayılarını gösterme  
- Envanterde mevcut cihaz sayısı  

### 📌 Yetkilendirme ve Roller
- Rol bazlı view gösterimi  
- Yetki tabanlı erişim sistemi  
- Sadece admin kullanıcıların erişebildiği **Ayarlar ekranı**:
  - Kullanıcı kaydı  
  - Kullanıcı listesi  
  - Rol ekleme  

---

## 🏛️ Mimari Yapı

Proje **Katmanlı Mimari (Layered Architecture)** yapısına göre tasarlanmıştır. Katmanlar arasındaki sorumluluklar şu şekildedir:

- **Domain (Alan Katmanı)** → İş kuralları ve entity tanımları  
- **Application (Uygulama Katmanı)** → Servisler, DTO ve ViewModel yapıları, iş mantığının uygulanması  
- **Infrastructure (Altyapı Katmanı)** → Veritabanı erişimi (EF Core, DbContext, repository implementasyonları)  
- **Presentation (Sunum Katmanı - Web UI)** → ASP.NET Core MVC tabanlı kullanıcı arayüzü  

---

## 📂 Proje Yapısı

```bash
├── destek-kayit-sistemi (Sunum Katmanı - Web UI)
│   ├── Connected Services
│   ├── Properties
│   ├── wwwroot               # Statik dosyalar (CSS, JS, img)
│   ├── Controllers           # HTTP isteklerini yöneten controller sınıfları
│   ├── Views                 # Razor view sayfaları
│   ├── appsettings.json      # Uygulama ayarları ve connection string
│   └── Program.cs            # Uygulama başlangıç noktası
│
├── destekkayitsistemi.Application (Uygulama Katmanı)
│   ├── DTOs                  # Veri transfer nesneleri
│   ├── Interfaces            # Servis arayüzleri
│   ├── Services              # İş mantığını içeren servisler
│   └── ViewModels            # Sunum katmanı için özel modeller
│
├── destekkayitsistemi.Domain (Alan Katmanı)
│   ├── Entities              # Veritabanı tablolarına karşılık gelen entity sınıfları
│
└── destekkayitsistemi.Infrastructure (Altyapı Katmanı)
    ├── Data                  # ApplicationDbContext ve repository implementasyonları
    └── Migrations            # Veritabanı migration dosyaları
