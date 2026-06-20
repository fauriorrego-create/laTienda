using Microsoft.EntityFrameworkCore;
using laTienda.Models;

var builder = WebApplication.CreateBuilder(args);

// SERVICES
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("cadenaSQL");

builder.Services.AddDbContext<PruebaContext>(options =>
{
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(8, 0, 36))
    );
});

var app = builder.Build();

// PIPELINE CORRECTO
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

// Health check
app.MapGet("/", () => "API funcionando 🚀");

// ❌ NO fijar puerto en Render
app.Run();