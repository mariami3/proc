using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace proctos.Models;

public partial class BrandsHopContext : DbContext
{
    public BrandsHopContext()
    {
    }

    public BrandsHopContext(DbContextOptions<BrandsHopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoryOfGender> CategoryOfGenders { get; set; }

    public virtual DbSet<CategoryOfProduct> CategoryOfProducts { get; set; }

    public virtual DbSet<Colour> Colours { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=WIN-5O8Q88327DH\\SQLEXPRESS01;Initial Catalog=BRANDsHOP;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryOfGender>(entity =>
        {
            entity.HasKey(e => e.IdCategoryOfGender).HasName("PK__Category__0DD71A430C4F7F3F");

            entity.ToTable("CategoryOfGender");

            entity.Property(e => e.IdCategoryOfGender).HasColumnName("ID_CategoryOfGender");
            entity.Property(e => e.NameCategoryOfGender)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CategoryOfProduct>(entity =>
        {
            entity.HasKey(e => e.IdCategoryOfProduct).HasName("PK__Category__AD906E0DC3D084B2");

            entity.ToTable("CategoryOfProduct");

            entity.Property(e => e.IdCategoryOfProduct).HasColumnName("ID_CategoryOfProduct");
            entity.Property(e => e.NameCategoryOfProduct)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Colour>(entity =>
        {
            entity.HasKey(e => e.IdColour).HasName("PK__Colour__8F158169761571D2");

            entity.ToTable("Colour");

            entity.Property(e => e.IdColour).HasColumnName("ID_Colour");
            entity.Property(e => e.ColourName)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.IdMaterial).HasName("PK__Material__A7F521BB97DDCBA4");

            entity.ToTable("Material");

            entity.Property(e => e.IdMaterial).HasColumnName("ID_Material");
            entity.Property(e => e.Material1)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Material");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PK__Orders__EC9FA955D03C33C1");

            entity.Property(e => e.IdOrder).HasColumnName("ID_Order");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.SumOfOrder).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UsersId).HasColumnName("Users_ID");

            entity.HasOne(d => d.Product).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Orders__Product___5AB9788F");

            entity.HasOne(d => d.Users).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UsersId)
                .HasConstraintName("FK__Orders__Users_ID__5BAD9CC8");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.IdOrderDetail).HasName("PK__OrderDet__855D4EF519BFBBCE");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.IdOrderDetail).HasColumnName("ID_OrderDetail");
            entity.Property(e => e.OrderId).HasColumnName("Order_ID");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__5F7E2DAC");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__OrderDeta__Produ__5E8A0973");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PK__Product__522DE49642320939");

            entity.ToTable("Product");

            entity.Property(e => e.IdProduct).HasColumnName("ID_Product");
            entity.Property(e => e.CategoryOfGenderId).HasColumnName("CategoryOfGender_ID");
            entity.Property(e => e.CategoryOfProductId).HasColumnName("CategoryOfProduct_ID");
            entity.Property(e => e.ColourId).HasColumnName("Colour_ID");
            entity.Property(e => e.ImageOfProduct)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.MaterialId).HasColumnName("Material_ID");
            entity.Property(e => e.NameProduct)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SizeId).HasColumnName("Size_ID");

            entity.HasOne(d => d.CategoryOfGender).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryOfGenderId)
                .HasConstraintName("FK__Product__Categor__787EE5A0");

            entity.HasOne(d => d.CategoryOfProduct).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryOfProductId)
                .HasConstraintName("FK__Product__Categor__778AC167");

            entity.HasOne(d => d.Colour).WithMany(p => p.Products)
                .HasForeignKey(d => d.ColourId)
                .HasConstraintName("FK__Product__Colour___7A672E12");

            entity.HasOne(d => d.Material).WithMany(p => p.Products)
                .HasForeignKey(d => d.MaterialId)
                .HasConstraintName("FK__Product__Materia__7B5B524B");

            entity.HasOne(d => d.Size).WithMany(p => p.Products)
                .HasForeignKey(d => d.SizeId)
                .HasConstraintName("FK__Product__Size_ID__797309D9");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PK__Roles__43DCD32D2AE3D299");

            entity.Property(e => e.IdRole).HasColumnName("ID_Role");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.IdSize).HasName("PK__Size__EB77DAF0D1900F40");

            entity.ToTable("Size");

            entity.Property(e => e.IdSize).HasColumnName("ID_Size");
            entity.Property(e => e.Size1).HasColumnName("Size");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__Users__ED4DE4428409EF95");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534604E2BF4").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("ID_User");
            entity.Property(e => e.DateJoined)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LoginUser)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("Role_ID");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__Role_ID__57DD0BE4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
