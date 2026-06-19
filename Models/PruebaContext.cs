using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace laTienda.Models;

public partial class PruebaContext : DbContext
{
    public PruebaContext()
    {
    }

    public PruebaContext(DbContextOptions<PruebaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Usuariorole> Usuarioroles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=prueba;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Idcategoria).HasName("PRIMARY");

            entity.ToTable("categorias");

            entity.HasIndex(e => e.Nombrecategoria, "nombrecategoria").IsUnique();

            entity.Property(e => e.Idcategoria)
                .HasColumnType("int(11)")
                .HasColumnName("idcategoria");
            entity.Property(e => e.Estadocategoria)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("estadocategoria");
            entity.Property(e => e.Nombrecategoria)
                .HasMaxLength(100)
                .HasColumnName("nombrecategoria");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Idproducto).HasName("PRIMARY");

            entity.ToTable("productos");

            entity.HasIndex(e => e.Idcategoria, "FK_Productos_Categorias");

            entity.HasIndex(e => e.Nombreproducto, "nombreproducto").IsUnique();

            entity.Property(e => e.Idproducto)
                .HasColumnType("int(11)")
                .HasColumnName("idproducto");
            entity.Property(e => e.Estadoproducto)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("estadoproducto");
            entity.Property(e => e.Idcategoria)
                .HasColumnType("int(11)")
                .HasColumnName("idcategoria");
            entity.Property(e => e.Nombreproducto)
                .HasMaxLength(150)
                .HasColumnName("nombreproducto");
            entity.Property(e => e.Precioproducto)
                .HasPrecision(15, 2)
                .HasColumnName("precioproducto");
            entity.Property(e => e.Stockproducto)
                .HasColumnType("int(11)")
                .HasColumnName("stockproducto");

            entity.HasOne(d => d.IdcategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.Idcategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Categorias");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Idrol).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Nombrerol, "nombrerol").IsUnique();

            entity.Property(e => e.Idrol)
                .HasColumnType("int(11)")
                .HasColumnName("idrol");
            entity.Property(e => e.Nombrerol)
                .HasMaxLength(50)
                .HasColumnName("nombrerol");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.Nrodoc, "nrodoc").IsUnique();

            entity.Property(e => e.Idusuario)
                .HasColumnType("int(11)")
                .HasColumnName("idusuario");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasColumnName("nombre");
            entity.Property(e => e.Nrodoc)
                .HasMaxLength(13)
                .HasColumnName("nrodoc");
            entity.Property(e => e.Passwordhash).HasColumnName("passwordhash");
            entity.Property(e => e.Passwordsalt).HasColumnName("passwordsalt");
            entity.Property(e => e.Tipodoc)
                .HasMaxLength(3)
                .HasColumnName("tipodoc");
        });

        modelBuilder.Entity<Usuariorole>(entity =>
        {
            entity.HasKey(e => e.Idusuariorol).HasName("PRIMARY");

            entity.ToTable("usuarioroles");

            entity.HasIndex(e => e.Idrol, "FK_UsuarioRoles_Roles");

            entity.HasIndex(e => new { e.Idusuario, e.Idrol }, "idusuario").IsUnique();

            entity.Property(e => e.Idusuariorol)
                .HasColumnType("int(11)")
                .HasColumnName("idusuariorol");
            entity.Property(e => e.Idrol)
                .HasColumnType("int(11)")
                .HasColumnName("idrol");
            entity.Property(e => e.Idusuario)
                .HasColumnType("int(11)")
                .HasColumnName("idusuario");

            entity.HasOne(d => d.IdrolNavigation).WithMany(p => p.Usuarioroles)
                .HasForeignKey(d => d.Idrol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioRoles_Roles");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Usuarioroles)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioRoles_Usuarios");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
