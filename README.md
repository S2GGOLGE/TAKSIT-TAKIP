💳 TAKSİT TAKİP SİSTEMİ

Modern ve kullanıcı dostu bir taksit / borç takip uygulaması.
Kullanıcıların yaptığı alışverişleri, borçlarını ve ödeme planlarını düzenli şekilde yönetmesini sağlar.

🚀 Özellikler
📌 Taksit ekleme ve yönetme
📅 Ödeme tarihlerini takip etme
💰 Toplam borç ve kalan tutar hesaplama
📊 Detaylı listeleme ve raporlama
🔔 Ödeme hatırlatma mantığı (geliştirilebilir)
👤 Kullanıcı bazlı veri yönetimi
🧾 Geçmiş ödeme kayıtları
🛠️ Kullanılan Teknolojiler
Backend: ASP.NET Core
Frontend: (WinForms / Web / Razor → hangisini kullandıysan burayı düzenle)
Database: SQL Server
ORM: Entity Framework Core
📂 Proje Yapısı
TAKSIT-TAKIP/
│
├── Controllers/       # API endpointleri
├── Models/            # Veri modelleri
├── Services/          # İş mantığı
├── Data/              # Veritabanı işlemleri
├── Views/             # UI (varsa)
└── Program.cs         # Uygulama başlangıcı
⚙️ Kurulum

Projeyi çalıştırmak için:

git clone https://github.com/S2GGOLGE/TAKSIT-TAKIP.git
cd TAKSIT-TAKIP
1. Veritabanını ayarla

appsettings.json içine kendi bağlantını gir:

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=TaksitDB;Trusted_Connection=True;"
}
2. Migration çalıştır
dotnet ef database update
3. Projeyi başlat
dotnet run
📸 Ekran Görüntüleri (Eklenebilir)

Buraya uygulama görselleri eklersen repo 10x daha profesyonel görünür

📌 Kullanım Senaryosu
Bir ürün aldın → taksit ekledin
Sistem sana:
kalan borcu gösterir
ödeme tarihlerini listeler
Ödeme yaptıkça sistem güncellenir

Bu sayede defter tutma derdi ortadan kalkar ve tüm finansal durum tek yerden yönetilir.

🔧 Geliştirilebilir Özellikler
🔔 Bildirim sistemi (SMS / Mail)
📱 Mobil uygulama entegrasyonu
📊 Grafik dashboard
☁️ Cloud deploy (Azure / Docker)
🔐 JWT Authentication sistemi
🤝 Katkıda Bulunma

Katkıda bulunmak istiyorsan:

Fork al
Feature branch aç
Commit at
Pull request gönder
📜 Lisans

Bu proje açık kaynaklıdır. İstediğin gibi geliştirebilirsin.

👨‍💻 Geliştirici

S2GGOLGE
