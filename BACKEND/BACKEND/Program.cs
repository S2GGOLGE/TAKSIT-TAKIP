using BACKEND;
using TAKSIT_TAKIP_BACKEND;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
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

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapPost("/api/login", (LoginRequest req) =>
{
    if (req is null)
        return Results.BadRequest(new { hata = "İstek boş geldi" });

    var login = new Login();
    string sonuc = login.Giris(req.Username, req.Password);

    if (sonuc == "OK")
    {
        Console.WriteLine($"GİRİŞ GELDİ {req.Username}");
        return Results.Ok(new { mesaj = "Giriş başarılı" });
    }

    return Results.BadRequest(new { hata = sonuc });
});

app.MapPost("/api/Kayıt", (KayıtRequest req) =>
{
    if (req == null)
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
app.Run();

record LoginRequest(string Username, string Password);
record KayıtRequest(string Date, Double Tutar, string Açıklama);