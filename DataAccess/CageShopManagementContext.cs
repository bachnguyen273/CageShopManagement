using System;
using System.Collections.Generic;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess;

public partial class CageShopManagementContext : DbContext
{
    public CageShopManagementContext()
    {
    }

    public CageShopManagementContext(DbContextOptions<CageShopManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BirdCage> BirdCages { get; set; }

    public virtual DbSet<BirdCageMaterial> BirdCageMaterials { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = GetConnectionString();
        optionsBuilder.UseSqlServer(connectionString);
    }

    public string GetConnectionString()
    {
        string connectionString;
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        connectionString = config.GetConnectionString("CageShopManagementDb");
        return connectionString;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BirdCage>(entity =>
        {
            entity.HasKey(e => e.CageId).HasName("PK__BirdCage__792D9FBA57DE4584");

            entity.ToTable("BirdCage");

            entity.Property(e => e.CageId).HasColumnName("CageID");
            entity.Property(e => e.Accessories).HasMaxLength(100);
            entity.Property(e => e.BirdType).HasMaxLength(30);
            entity.Property(e => e.CageName).HasMaxLength(30);
            entity.Property(e => e.ImagePath).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");

            entity.HasOne(d => d.CreatedByCustomerNavigation).WithMany(p => p.BirdCages).HasForeignKey(d => d.CreatedByCustomer);
        });

        modelBuilder.Entity<BirdCageMaterial>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("BirdCageMaterial");

            entity.Property(e => e.CageId).HasColumnName("CageID");
            entity.Property(e => e.MaterialId).HasColumnName("MaterialID");

            entity.HasOne(d => d.Cage).WithMany().HasForeignKey(d => d.CageId);

            entity.HasOne(d => d.Material).WithMany().HasForeignKey(d => d.MaterialId);
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("PK__Material__C50613178E85843C");

            entity.ToTable("Material");

            entity.Property(e => e.MaterialId).HasColumnName("MaterialID");
            entity.Property(e => e.MaterialName).HasMaxLength(20);
            entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__C3905BAFF44D1542");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.ReceiverName).HasMaxLength(50);
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(19, 4)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D30C12BC721F");

            entity.ToTable("OrderDetail", tb => tb.HasTrigger("CalculateOrderDetailSubtotal"));

            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");
            entity.Property(e => e.CageId).HasColumnName("CageID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(19, 4)");

            entity.HasOne(d => d.Cage).WithMany(p => p.OrderDetails).HasForeignKey(d => d.CageId);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE3A8DA452A5");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(20);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Status__C8EE204315FD08C9");

            entity.ToTable("Status");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.StatusName).HasMaxLength(20);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCACFCDB2C31");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(20);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UserName).HasMaxLength(20);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
