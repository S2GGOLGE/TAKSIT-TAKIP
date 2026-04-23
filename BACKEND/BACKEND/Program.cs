using BACKEND;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Text;
using TAKSIT_TAKIP_BACKEND;

// ─────────────────────────────────────────
// BUILDER YAPILANDIRMASI
// ─────────────────────────────────────────
var builder = WebApplication.CreateBuilder(args);

// PORTU ZORUNLU 7026 YAP
builder.WebHost.UseUrls("http://localhost:7026");

// JSON YAPILANDIRMASI
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
});

// SERVİSLER
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// CORS POLİTİKASI
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// ─────────────────────────────────────────
// MİDDLEWARE SIRASI
// ─────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection(); // Yerel testte sorun çıkarırsa yorum satırı yap
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthorization();

// ─────────────────────────────────────────
// ENDPOINT'LER
// ─────────────────────────────────────────

// LOGIN
app.MapPost("/api/login", ([FromBody] LoginRequest req) =>
{
    if (req is null)
        return Results.BadRequest(new { hata = "İstek boş geldi" });

    var login = new Login();
    string sonuc = login.Giris(req.Username, req.Password);

    if (sonuc == "OK")
    {
        Console.WriteLine($"GİRİŞ GELDİ: {req.Username}");
        return Results.Ok(new { mesaj = "Giriş başarılı" });
    }

    return Results.BadRequest(new { hata = sonuc });
});

// TAKSİT KAYIT
app.MapPost("/api/Kayit", ([FromBody] KayıtRequest req) =>
{
    if (req is null)
        return Results.BadRequest(new { mesaj = "Geçersiz istek" });

    taksit_kaydet.Date = req.Date;
    taksit_kaydet.Tutar = req.Tutar;
    taksit_kaydet.Açıklama = req.Açıklama;

    string sonuc = new taksit_kaydet().Kayıt();

    return sonuc == "OK"
        ? Results.Ok(new { mesaj = "Taksit başarıyla kaydedildi" })
        : Results.BadRequest(new { mesaj = sonuc });
});

app.MapControllers();

// ─────────────────────────────────────────
// HOME SERVER BAĞLANTISI
// ─────────────────────────────────────────
try
{
    var homeClient = new TcpClient();
    await homeClient.ConnectAsync("192.168.1.115", 8586);

    string tanitim = "TAKSIT TAKIP BACKEND";
    byte[] data = Encoding.UTF8.GetBytes(tanitim);
    NetworkStream stream = homeClient.GetStream();
    await stream.WriteAsync(data, 0, data.Length);

    Console.WriteLine("HOME SUNUCUYA BAĞLANDI");
}
catch (Exception ex)
{
    Console.WriteLine($"HOME BAĞLANTI HATASI: {ex.Message}");
}

// ─────────────────────────────────────────
// UYGULAMAYI ÇALIŞTIR
// ─────────────────────────────────────────
app.Run();

// ─────────────────────────────────────────
// DTO TANIMLARI
// ─────────────────────────────────────────
public record LoginRequest(string Username, string Password);
public record KayıtRequest(string Date, double Tutar, string Açıklama);