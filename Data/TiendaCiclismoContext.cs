using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TiendaCiclismo.Models;

namespace TiendaCiclismo.Data
{
    public class TiendaCiclismoContext : IdentityDbContext<ApplicationUser>
    {
        public TiendaCiclismoContext(DbContextOptions<TiendaCiclismoContext> options)
            : base(options)
        {
        }

        // Definición de las tablas
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<FacturaEntrada> FacturasEntrada { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de relaciones existentes
            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Vendedor)
                .WithMany(v => v.Facturas)
                .HasForeignKey(f => f.VendedorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Producto)
                .WithMany(p => p.Facturas)
                .HasForeignKey(f => f.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación entre Producto y Proveedor
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Proveedor)
                .WithMany(pr => pr.Productos)
                .HasForeignKey(p => p.ProveedorId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configuración de propiedades
            modelBuilder.Entity<Producto>().Property(p => p.Nombre).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Producto>().Property(p => p.Precio).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Producto>().Property(p => p.Descripcion).HasMaxLength(500);
            modelBuilder.Entity<Vendedor>().Property(v => v.Nombre).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Factura>().Property(f => f.Total).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Proveedor>().Property(p => p.Nombre).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Proveedor>().Property(p => p.Contacto).HasMaxLength(100);
            modelBuilder.Entity<Proveedor>().Property(p => p.Direccion).HasMaxLength(200);

            modelBuilder.Entity<Compra>().Property(c => c.Total).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<FacturaEntrada>().Property(f => f.Total).HasColumnType("decimal(18,2)");

            // Configuración de Inventario
            modelBuilder.Entity<Inventario>()
                .Property(i => i.TotalVendido)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Inventario>()
                .HasOne(i => i.Producto)
                .WithMany(p => p.Inventarios)
                .HasForeignKey(i => i.ProductoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Inventario>()
                .HasOne(i => i.Vendedor)
                .WithMany(v => v.Inventarios)
                .HasForeignKey(i => i.VendedorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
