using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ASPLab05.Dbcontext;

public partial class CompanyDbContext : DbContext
{
    public CompanyDbContext()
    {
    }

    public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-MT9ID2NC\\SQLEXPRESS;Database=CompanyDB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Account");

            entity.Property(e => e.AccountType)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Password).HasMaxLength(30);
            entity.Property(e => e.UserName).HasMaxLength(20);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07CB18474A");

            entity.ToTable("Employee");

            entity.Property(e => e.City)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Salary)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("salary");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Product");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Productname)
                .HasMaxLength(50)
                .HasColumnName("productname");
            entity.Property(e => e.Quality).HasColumnName("quality");
            entity.Property(e => e.Total).HasColumnName("total");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
