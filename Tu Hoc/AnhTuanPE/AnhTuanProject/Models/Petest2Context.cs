using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AnhTuanProject.Models;

public partial class Petest2Context : DbContext
{
    public Petest2Context()
    {
    }

    public Petest2Context(DbContextOptions<Petest2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Table> Tables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ANHTUAN;Initial Catalog=PETest2;User ID=sa;Password=123456;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Table__3214EC07BC6704D3");

            entity.ToTable("Table");

            entity.Property(e => e.Departure).HasMaxLength(255);
            entity.Property(e => e.DepartureDate).HasMaxLength(50);
            entity.Property(e => e.DepartureTime).HasMaxLength(50);
            entity.Property(e => e.Destination).HasMaxLength(255);
            entity.Property(e => e.FlightNumber).HasMaxLength(255);
            entity.Property(e => e.PasserngerName).HasMaxLength(255);
            entity.Property(e => e.SeatClass).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
