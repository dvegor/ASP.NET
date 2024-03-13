using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Market.Models
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Storage> Storages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=STEALTH; Database=Market; Integrated Security=true; TrustServerCertificate=True").UseLazyLoadingProxies(); // connection string for ms sql
            //optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=pass0+word;Database=Market").UseLazyLoadingProxies(); -connectionstring for Postgres
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");

                entity.HasKey(x => x.Id).HasName("ProductID");
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.Name)
                    .HasColumnName("ProductName")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.Description)
                .HasColumnName("Description")
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(e => e.Price)
                    .HasColumnName("Price")
                    .IsRequired();

                entity.HasOne(x => x.ProductGroup)
                .WithMany(c => c.Products)
                .HasForeignKey(x => x.ProductId)
                .HasConstraintName("ProductToGroup");

            });

            modelBuilder.Entity<ProductGroup>(entity =>
            {
                entity.ToTable("ProductGroup");

                entity.HasKey(x => x.Id).HasName("GroupId");
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.Name)
                .HasColumnName("ProductName")
                .HasMaxLength(255)
                .IsRequired();

            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.ToTable("Storage");
                entity.HasKey(x => x.Id).HasName("StorageID");

                entity.Property(e => e.Name)
                .HasColumnName("StorageName");

                entity.Property(e => e.Count)
                .HasColumnName("ProductCount");

                entity.HasMany(x => x.Products)
                .WithMany(m => m.Storages)
                .UsingEntity(j => j.ToTable("ProductsToStorages"));

            });
        }
    }
}
