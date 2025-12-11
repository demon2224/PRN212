using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WPF_Tu_Hoc.Entities;

namespace WPF_Tu_Hoc;

public partial class TuHocContext : DbContext
{
    public TuHocContext()
    {
    }

    public TuHocContext(DbContextOptions<TuHocContext> options)
        : base(options)
    {
    }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<Sach> Saches { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(local);Database=tuHoc;UID=sa;PWD=123456;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__khachHan__3213E83F4CA7A09E");

            entity.ToTable("khachHang");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("diaChi");
            entity.Property(e => e.HoVaTen)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hoVaTen");
            entity.Property(e => e.NgaySinh).HasColumnName("ngaySinh");
        });

        modelBuilder.Entity<Sach>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sach__3213E83FB8C002F4");

            entity.ToTable("sach");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.GiaBan)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("giaBan");
            entity.Property(e => e.NamXuatBan).HasColumnName("namXuatBan");
            entity.Property(e => e.TenSach)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("tenSach");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
