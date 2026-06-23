using Microsoft.EntityFrameworkCore;
using laTienda.Models;
using laTienda.Services;
using laTienda.Mappings;

var builder = WebApplication.CreateBuilder(args);

// =====================
// SERVICES
// =====================

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<JwtService>();

// =====================
// DATABASE
// =====================

var connectionString = builder.Configuration.GetConnectionString("cadenaSQL");

builder.Services.AddDbContext<PruebaContext>(options =>
{
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    );
});

// =====================
// APP BUILD
// =====================

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "API funcionando 🚀");

app.Run();