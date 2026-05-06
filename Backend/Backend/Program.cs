using Backend.LoginDto;
using Backend.SQL;
using System.Net.Sockets;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

#region SERVICES

// ==========================
// CONTROLLERS
// ==========================
builder.Services.AddControllers();

#endregion

#region CORS

// ==========================
// CORS
// ==========================
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:3000",
                "http://localhost:5173",
                "http://127.0.0.1:5500",
                "http://localhost:5500"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

#endregion

var app = builder.Build();

#region MIDDLEWARE

// ==========================
// HTTPS
// ==========================
app.UseHttpsRedirection();

// ==========================
// CORS
// ==========================
app.UseCors("Frontend");

// ==========================
// AUTH
// ==========================
app.UseAuthorization();

#endregion

#region ENDPOINTS

// ==========================
// CONTROLLERS
// ==========================
app.MapControllers();

#endregion
#region LOGİN
//Login Buraya
#endregion
#region HOME SERVER BAĞLANTI
try
{
    var homeClient = new TcpClient();
await homeClient.ConnectAsync("127.0.0.1", 8587);

string tanitim = "GYM PRO";
byte[] data = Encoding.UTF8.GetBytes(tanitim);

NetworkStream stream = homeClient.GetStream();
await stream.WriteAsync(data, 0, data.Length);

Console.WriteLine("GYM-PRO SUNUCUYA BAĞLANDI");
}
catch (Exception ex)
{
    Console.WriteLine($"GYM-PRO BAĞLANTI HATASI: {ex.Message}");
}
#endregion
app.Run();