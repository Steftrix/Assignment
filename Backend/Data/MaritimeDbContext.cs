using System;
using System.Collections.Generic;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public partial class MaritimeDbContext : DbContext
{
    public MaritimeDbContext(DbContextOptions<MaritimeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Lastyearcountry> Lastyearcountries { get; set; }

    public virtual DbSet<Port> Ports { get; set; }

    public virtual DbSet<Ship> Ships { get; set; }

    public virtual DbSet<Voyage> Voyages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("countries_pkey");

            entity.ToTable("countries");

            entity.HasIndex(e => e.CountryName, "countries_country_name_key").IsUnique();

            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CountryName)
                .HasMaxLength(100)
                .HasColumnName("country_name");
        });

        modelBuilder.Entity<Lastyearcountry>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("lastyearcountries");

            entity.Property(e => e.CountryName)
                .HasMaxLength(100)
                .HasColumnName("country_name");
            entity.Property(e => e.PortsVisited).HasColumnName("ports_visited");
            entity.Property(e => e.VisitCount).HasColumnName("visit_count");
        });

        modelBuilder.Entity<Port>(entity =>
        {
            entity.HasKey(e => e.PortId).HasName("ports_pkey");

            entity.ToTable("ports");

            entity.Property(e => e.PortId).HasColumnName("port_id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.PortName)
                .HasMaxLength(255)
                .HasColumnName("port_name");

            entity.HasOne(d => d.Country).WithMany(p => p.Ports)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("ports_country_id_fkey");
        });

        modelBuilder.Entity<Ship>(entity =>
        {
            entity.HasKey(e => e.ShipId).HasName("ships_pkey");

            entity.ToTable("ships");

            entity.HasIndex(e => e.ShipType, "idx_ships_type");

            entity.Property(e => e.ShipId).HasColumnName("ship_id");
            entity.Property(e => e.MaxSpeed)
                .HasPrecision(8, 2)
                .HasColumnName("max_speed");
            entity.Property(e => e.ShipName)
                .HasMaxLength(255)
                .HasColumnName("ship_name");
            entity.Property(e => e.ShipType)
                .HasMaxLength(255)
                .HasColumnName("ship_type");
        });

        modelBuilder.Entity<Voyage>(entity =>
        {
            entity.HasKey(e => e.VoyageId).HasName("voyages_pkey");

            entity.ToTable("voyages");

            entity.HasIndex(e => new { e.VoyageStart, e.VoyageEnd }, "idx_voyages_dates").HasMethod("brin");

            entity.Property(e => e.VoyageId).HasColumnName("voyage_id");
            entity.Property(e => e.ArrivalPort).HasColumnName("arrival_port");
            entity.Property(e => e.DeparturePort).HasColumnName("departure_port");
            entity.Property(e => e.ShipId).HasColumnName("ship_id");
            entity.Property(e => e.VoyageEnd)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("voyage_end");
            entity.Property(e => e.VoyageStart)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("voyage_start");

            entity.HasOne(d => d.ArrivalPortNavigation).WithMany(p => p.VoyageArrivalPortNavigations)
                .HasForeignKey(d => d.ArrivalPort)
                .HasConstraintName("voyages_arrival_port_fkey");

            entity.HasOne(d => d.DeparturePortNavigation).WithMany(p => p.VoyageDeparturePortNavigations)
                .HasForeignKey(d => d.DeparturePort)
                .HasConstraintName("voyages_departure_port_fkey");

            entity.HasOne(d => d.Ship).WithMany(p => p.Voyages)
                .HasForeignKey(d => d.ShipId)
                .HasConstraintName("voyages_ship_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
