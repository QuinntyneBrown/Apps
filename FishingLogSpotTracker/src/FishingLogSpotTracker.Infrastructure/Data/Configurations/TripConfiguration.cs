// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FishingLogSpotTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishingLogSpotTracker.Infrastructure;

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

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.TripDate)
            .IsRequired();

        builder.Property(x => x.StartTime)
            .IsRequired();

        builder.Property(x => x.WeatherConditions)
            .HasMaxLength(500);

        builder.Property(x => x.WaterTemperature)
            .HasPrecision(5, 2);

        builder.Property(x => x.AirTemperature)
            .HasPrecision(5, 2);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.Catches)
            .WithOne(x => x.Trip)
            .HasForeignKey(x => x.TripId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.TripDate);
        builder.HasIndex(x => new { x.UserId, x.TripDate });
    }
}
