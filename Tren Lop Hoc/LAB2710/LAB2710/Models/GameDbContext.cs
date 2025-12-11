using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LAB2710.Configuration;

namespace LAB2710.Models;

public partial class GameDbContext : DbContext
{
    public GameDbContext()
    {
    }

    public GameDbContext(DbContextOptions<GameDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Game> Games { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = ConfigurationHelper.GetConnectionString("GameDB");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.GameId);

            entity.ToTable("Game");

            entity.Property(e => e.GameId)
                .HasColumnName("GameID")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Genre)
                .HasMaxLength(50)
                .IsUnicode(true);

            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode(true);

            entity.Property(e => e.Price)
                .HasColumnType("float");

            entity.Property(e => e.ReleaseDate)
                .HasColumnType("datetime2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
