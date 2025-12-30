// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FuelEconomyTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Trip entity.
/// </summary>
public class TripConfiguration : IEntityTypeConfiguration<Trip>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.ToTable("Trips");

        builder.HasKey(x => x.TripId);

        builder.Property(x => x.TripId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.VehicleId)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.StartOdometer)
            .IsRequired();

        builder.Property(x => x.EndOdometer);

        builder.Property(x => x.TripType)
            .IsRequired();

        builder.Property(x => x.Purpose)
            .HasMaxLength(500);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.VehicleId);
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.StartDate);
        builder.HasIndex(x => new { x.VehicleId, x.TripType });
    }
}
