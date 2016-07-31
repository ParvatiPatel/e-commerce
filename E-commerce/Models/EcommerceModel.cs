namespace E_commerce.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EcommerceModel : DbContext
    {
        public EcommerceModel()
            : base("name=EcommerceConnection")
        {
        }

        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Item>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Item>()
                .Property(e => e.Price)
                .HasPrecision(6, 2);

            modelBuilder.Entity<Item>()
                .Property(e => e.ThumbUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Item>()
                .Property(e => e.OriginalUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Item>()
                .Property(e => e.Tag)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ThumbUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Items)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);
        }
    }
}
