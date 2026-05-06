using Backend.LoginDto;

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
app.UseAuthentication();
app.UseAuthorization();

#endregion

#region ENDPOINTS

// ==========================
// CONTROLLERS
// ==========================
app.MapControllers();

#endregion

#region LOGIN

// Login işlemleri buraya gelecek

#endregion

#region RUN

app.Run();

#endregion