// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FuelEconomyTracker.Infrastructure;

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

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Make)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Model)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Year)
            .IsRequired();

        builder.Property(x => x.VIN)
            .HasMaxLength(17);

        builder.Property(x => x.LicensePlate)
            .HasMaxLength(20);

        builder.Property(x => x.FuelType)
            .IsRequired();

        builder.Property(x => x.TankCapacity)
            .HasPrecision(5, 2);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.FillUps)
            .WithOne(x => x.Vehicle)
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Trips)
            .WithOne(x => x.Vehicle)
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.EfficiencyReports)
            .WithOne(x => x.Vehicle)
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => new { x.UserId, x.IsActive });
        builder.HasIndex(x => x.VIN);
    }
}
