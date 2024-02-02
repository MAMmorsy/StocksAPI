using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StocksAPI.CORE.Models.Entities;

namespace StocksAPI.EF.Context
{
    public partial class InvoiceDbContext : DbContext
    {
        public InvoiceDbContext()
        {
        }

        public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<InvoiceItem> InvoiceItems { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductUnit> ProductUnits { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Store> Stores { get; set; } = null!;
        public virtual DbSet<StoreProduct> StoreProducts { get; set; } = null!;
        public virtual DbSet<Unit> Units { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=WIN-3TN4HGAKL8H; user id=sa; password=SQL$erver;Database=Invoice;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.Datein).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoices_Users");
            });

            modelBuilder.Entity<InvoiceItem>(entity =>
            {
                entity.Property(e => e.Datein).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceItems)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceItems_Invoices");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.InvoiceItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceItems_Products");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Datein).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ProductUnit>(entity =>
            {
                entity.Property(e => e.Datein).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductUnits)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductUnits_Products");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.ProductUnits)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductUnits_Units");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Datein).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(e => e.Datein).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<StoreProduct>(entity =>
            {
                entity.Property(e => e.Datein).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.StoreProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreProducts_Products");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreProducts)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreProducts_Stores");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.Property(e => e.Datein).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Datein).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
