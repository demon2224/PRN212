using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MvvmPEProject.Models;

public partial class LongHoPeContext : DbContext
{
    public LongHoPeContext()
    {
    }

    public LongHoPeContext(DbContextOptions<LongHoPeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FlightBooking> FlightBookings { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(local);Database=LongHoPE;UID=sa;PWD=123456;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FlightBooking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FlightBo__3214EC2758600E99");

            entity.ToTable("FlightBooking");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Departure).HasMaxLength(255);
            entity.Property(e => e.DepartureDate).HasMaxLength(50);
            entity.Property(e => e.DepartureTime).HasMaxLength(50);
            entity.Property(e => e.Destination).HasMaxLength(255);
            entity.Property(e => e.FlightNumber).HasMaxLength(50);
            entity.Property(e => e.PasserngerName).HasMaxLength(255);
            entity.Property(e => e.SeatClass).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
