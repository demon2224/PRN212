using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AnhTuanPEChuan.Models;

public partial class WatchDb2024Context : DbContext
{
    public WatchDb2024Context()
    {
    }

    public WatchDb2024Context(DbContextOptions<WatchDb2024Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Watch> Watches { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ANHTUAN;Initial Catalog=WatchDB2024;User ID=sa;Password=123456;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.ToTable("Brand");

            entity.Property(e => e.BrandId)
                .HasMaxLength(2)
                .IsFixedLength();
            entity.Property(e => e.BrandName).HasMaxLength(50);
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.ToTable("Member");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        modelBuilder.Entity<Watch>(entity =>
        {
            entity.ToTable("Watch");

            entity.Property(e => e.BrandId)
                .HasMaxLength(2)
                .IsFixedLength();
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.WatchName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
