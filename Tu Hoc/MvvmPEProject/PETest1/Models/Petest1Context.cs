using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PETest1.Models;

public partial class Petest1Context : DbContext
{
    public Petest1Context()
    {
    }

    public Petest1Context(DbContextOptions<Petest1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Table> Tables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ANHTUAN;Initial Catalog=PETest1;User ID=sa;Password=123456;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Table__3214EC076B8B076F");

            entity.ToTable("Table");

            entity.Property(e => e.Departure).HasMaxLength(255);
            entity.Property(e => e.DepartureDate).HasMaxLength(255);
            entity.Property(e => e.DepartureTime).HasMaxLength(255);
            entity.Property(e => e.Destination).HasMaxLength(255);
            entity.Property(e => e.FlightNumber).HasMaxLength(50);
            entity.Property(e => e.PassengerName).HasMaxLength(255);
            entity.Property(e => e.SeatClass).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
