// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using VehicleValueTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VehicleValueTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Vehicle entity.
/// </summary>
public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");

        builder.HasKey(x => x.VehicleId);

        builder.Property(x => x.VehicleId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Make)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Model)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Year)
            .IsRequired();

        builder.Property(x => x.Trim)
            .HasMaxLength(100);

        builder.Property(x => x.VIN)
            .HasMaxLength(17);

        builder.Property(x => x.CurrentMileage)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.PurchasePrice)
            .HasPrecision(18, 2);

        builder.Property(x => x.Color)
            .HasMaxLength(50);

        builder.Property(x => x.InteriorType)
            .HasMaxLength(100);

        builder.Property(x => x.EngineType)
            .HasMaxLength(100);

        builder.Property(x => x.Transmission)
            .HasMaxLength(100);

        builder.Property(x => x.IsCurrentlyOwned)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.HasMany(x => x.ValueAssessments)
            .WithOne(x => x.Vehicle)
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.MarketComparisons)
            .WithOne(x => x.Vehicle)
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.VIN);
        builder.HasIndex(x => new { x.Make, x.Model, x.Year });
        builder.HasIndex(x => x.IsCurrentlyOwned);
    }
}
