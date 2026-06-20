using Microsoft.EntityFrameworkCore;
using laTienda.Models;

var builder = WebApplication.CreateBuilder(args);

// =======================
// SERVICES
// =======================

// Controllers
builder.Services.AddControllers();

// Connection string
var connectionString = builder.Configuration.GetConnectionString("cadenaSQL");

// DbContext (sin AutoDetect, seguro para Render)
builder.Services.AddDbContext<PruebaContext>(options =>
{
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(8, 0, 36))
    );
});

var app = builder.Build();

// =======================
// PIPELINE
// =======================

// Ruta de prueba (para saber si la API está viva)
app.MapGet("/", () => "API funcionando 🚀");

// Controllers
app.MapControllers();

// Auth middleware (si lo usas)
app.UseAuthorization();

// =======================
// PORT (OBLIGATORIO EN RENDER)
// =======================
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";

app.Run($"http://0.0.0.0:{port}");