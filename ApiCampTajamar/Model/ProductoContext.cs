using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace ApiCampTajamar.Model
{
    public class ProductoContext:DbContext
    {
        public ProductoContext(DbContextOptions<ProductoContext> options) : base(options) { }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Tienda> Tienda { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<ProductoTienda> ProductoTienda { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductoTienda>()
                .HasKey(pc => new { pc.IdProducto, pc.IdTienda });
            
            modelBuilder.Entity<ProductoTienda>()
                .HasOne(pc => pc.Producto)
                .WithMany(p => p.ProductoTienda)
                .HasForeignKey(pc => pc.IdProducto);

            modelBuilder.Entity<ProductoTienda>()
                .HasOne(pc => pc.Tienda)
                .WithMany(c => c.ProductoTienda)
                .HasForeignKey(pc => pc.IdTienda);

        }
    }
}
