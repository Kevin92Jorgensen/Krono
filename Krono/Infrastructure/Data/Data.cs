using Krono.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Krono.Infrastructure.Data
{
    public class KronoDbContext : DbContext
    {
        public KronoDbContext(DbContextOptions<KronoDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<PriceEntry> PriceEntries { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Barcode> Barcodes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<Shop>()
                .HasIndex(s => s.Name)
                .IsUnique();
            modelBuilder.Entity<Barcode>()
                .HasKey(b => b.Id); // Primary key

            modelBuilder.Entity<Barcode>()
                .Property(b => b.Id)
                .ValueGeneratedOnAdd(); // Auto-increment

            modelBuilder.Entity<PriceEntry>()
                .HasOne(p => p.Product)
                .WithMany(p => p.PriceEntries)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
