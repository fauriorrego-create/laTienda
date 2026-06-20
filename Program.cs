using Microsoft.EntityFrameworkCore;
using laTienda.Models;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// 🔥 Leer connection string desde appsettings o variables de entorno (Render)
var connectionString = builder.Configuration.GetConnectionString("cadenaSQL");

// 🧠 DbContext corregido (SIN AutoDetect que rompe en Render)
builder.Services.AddDbContext<PruebaContext>(options =>
{
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(8, 0, 36))
    );
});

var app = builder.Build();

// ❌ En Render normalmente no es necesario y puede causar problemas
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();