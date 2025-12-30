// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using VehicleMaintenanceLogger.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VehicleMaintenanceLogger.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the MaintenanceSchedule entity.
/// </summary>
public class MaintenanceScheduleConfiguration : IEntityTypeConfiguration<MaintenanceSchedule>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<MaintenanceSchedule> builder)
    {
        builder.ToTable("MaintenanceSchedules");

        builder.HasKey(x => x.MaintenanceScheduleId);

        builder.Property(x => x.MaintenanceScheduleId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.VehicleId)
            .IsRequired();

        builder.Property(x => x.ServiceType)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.MileageInterval)
            .HasPrecision(18, 2);

        builder.Property(x => x.LastServiceMileage)
            .HasPrecision(18, 2);

        builder.Property(x => x.NextServiceMileage)
            .HasPrecision(18, 2);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.HasIndex(x => x.VehicleId);
        builder.HasIndex(x => x.NextServiceDate);
        builder.HasIndex(x => new { x.VehicleId, x.IsActive });
    }
}
