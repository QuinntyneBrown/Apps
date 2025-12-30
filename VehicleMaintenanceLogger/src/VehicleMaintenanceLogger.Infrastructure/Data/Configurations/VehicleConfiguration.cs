// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using VehicleMaintenanceLogger.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VehicleMaintenanceLogger.Infrastructure;

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

        builder.Property(x => x.VIN)
            .HasMaxLength(17);

        builder.Property(x => x.LicensePlate)
            .HasMaxLength(20);

        builder.Property(x => x.VehicleType)
            .IsRequired();

        builder.Property(x => x.CurrentMileage)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasMany(x => x.ServiceRecords)
            .WithOne(x => x.Vehicle)
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.MaintenanceSchedules)
            .WithOne(x => x.Vehicle)
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.VIN);
        builder.HasIndex(x => x.LicensePlate);
        builder.HasIndex(x => new { x.Make, x.Model, x.Year });
    }
}
