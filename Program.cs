using Microsoft.EntityFrameworkCore;
using laTienda.Models;
using laTienda.Services;
using laTienda.Mappings;

var builder = WebApplication.CreateBuilder(args);

// =====================
// SERVICES
// =====================

builder.Services.AddControllers();

// 🔥 AutoMapper (OBLIGATORIO)
builder.Services.AddAutoMapper(typeof(MappingProfile));

// 🔥 JwtService (OBLIGATORIO)
builder.Services.AddScoped<JwtService>();

// =====================
// DATABASE
// =====================

var connectionString = builder.Configuration.GetConnectionString("cadenaSQL");

builder.Services.AddDbContext<PruebaContext>(options =>
{
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(8, 0, 36))
    );
});

// =====================
// APP BUILD
// =====================

var app = builder.Build();

// =====================
// PIPELINE
// =====================

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

// Health check (opcional)
app.MapGet("/", () => "API funcionando 🚀");

app.Run();