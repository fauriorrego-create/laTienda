using Microsoft.EntityFrameworkCore;

namespace laTienda.Models;

public partial class PruebaContext : DbContext
{
    public PruebaContext(DbContextOptions<PruebaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }
    public virtual DbSet<Producto> Productos { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<Usuariorole> Usuarioroles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

        // 🔥 IMPORTANTE
        base.OnModelCreating(modelBuilder);

        // aquí mantienes tu mapping existente
    }
}