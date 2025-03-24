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

        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Vendedor)
                .WithMany(v => v.Facturas)
                .HasForeignKey(f => f.VendedorId);

            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Producto)
                .WithMany(p => p.Facturas)
                .HasForeignKey(f => f.ProductoId);

            modelBuilder.Entity<Producto>().Property(p => p.Nombre).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Producto>().Property(p => p.Precio).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Producto>().Property(p => p.Descripcion).HasMaxLength(500);
            modelBuilder.Entity<Vendedor>().Property(v => v.Nombre).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Factura>().Property(f => f.Total).HasColumnType("decimal(18,2)");
        }
    }
}
