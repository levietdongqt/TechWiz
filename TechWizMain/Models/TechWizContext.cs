using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TechWizMain.Models
{
    public partial class TechWizContext : DbContext
    {
        public TechWizContext()
        {
        }

        public TechWizContext(DbContextOptions<TechWizContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bill> Bills { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<CategoryProduct> CategoryProducts { get; set; } = null!;
        public virtual DbSet<Discount> Discounts { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductBill> ProductBills { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=DESKTOP-3FQ63QJ;Database=TechWiz;User ID=sa;Password=thuhuy;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Bill__UserId__5FB337D6");
            });

            modelBuilder.Entity<CategoryProduct>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryProducts)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__CategoryP__Categ__534D60F1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CategoryProducts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__CategoryP__Produ__5441852A");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Feedback__UserId__5BE2A6F2");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.DiscountId)
                    .HasConstraintName("FK__Product__Discoun__5070F446");
            });

            modelBuilder.Entity<ProductBill>(entity =>
            {
                entity.HasOne(d => d.Bill)
                    .WithMany(p => p.ProductBills)
                    .HasForeignKey(d => d.BillId)
                    .HasConstraintName("FK__ProductBi__BillI__6383C8BA");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductBills)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__ProductBi__Produ__628FA481");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Review__ProductI__5812160E");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Review__UserId__59063A47");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
