# 📊 TAKSİT TAKİP SİSTEMİ

Bu proje, kullanıcıların borç ve taksit ödemelerini yönetebileceği ASP.NET Core tabanlı bir Web API sistemidir.

---

## 🚀 Özellikler

- JWT tabanlı kullanıcı doğrulama
- Kullanıcı kayıt ve giriş sistemi
- Taksit oluşturma, güncelleme, silme
- Kullanıcı bazlı taksit listeleme
- Ödeme durumu takibi (ödendi / bekliyor / gecikmiş)
- Katmanlı mimari (Controller - Service - Data)

---

## 🧱 Kullanılan Teknolojiler

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- C#

---

## 📁 Proje Yapısı

```
TAKSIT-TAKIP/
│
├── Controllers/
│   ├── AuthController.cs
│   ├── TaksitController.cs
│   ├── UserController.cs
│
├── Models/
│   ├── User.cs
│   ├── Taksit.cs
│
├── Services/
│   ├── AuthService.cs
│   ├── TaksitService.cs
│
├── Data/
│   ├── AppDbContext.cs
│
├── Program.cs
└── appsettings.json
```

---

## ⚙️ Kurulum

### 1. Projeyi klonla
```bash
git clone https://github.com/S2GGOLGE/TAKSIT-TAKIP.git
cd TAKSIT-TAKIP
```

---

### 2. Bağımlılıkları yükle
```bash
dotnet restore
```

---

### 3. Veritabanı bağlantısı

`appsettings.json` içine:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=TaksitTakipDB;Trusted_Connection=True;"
}
```

---

### 4. Migration oluştur ve veritabanını kur
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

### 5. Projeyi çalıştır
```bash
dotnet run
```

---

## 🔐 Authentication

JWT token sistemi kullanılır.

### Endpointler:
- POST `/api/auth/register`
- POST `/api/auth/login`

Login sonrası token ile protected endpoint’lere erişilir.

---

## 📌 API Örnekleri

### Taksit Ekle
```
POST /api/taksit
```

```json
{
  "title": "Telefon",
  "amount": 5000,
  "installmentCount": 10,
  "userId": 1
}
```

---

### Kullanıcı Taksitleri
```
GET /api/taksit/user/{id}
```

---

### Güncelle
```
PUT /api/taksit/{id}
```

---

### Sil
```
DELETE /api/taksit/{id}
```

---

## 📊 Durumlar

- ✔️ Paid (Ödendi)
- ⏳ Pending (Bekliyor)
- ❌ Overdue (Gecikmiş)

---

## 🧠 Geliştirme Fikirleri

- E-posta / SMS bildirim sistemi
- React / Flutter frontend
- Dashboard grafik sistemi
- Otomatik ödeme hatırlatma
- PDF / Excel export

---

## 📄 Lisans

Bu proje kişisel geliştirme amaçlıdır. Serbestçe kullanılabilir ve geliştirilebilir.
