// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FishingLogSpotTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishingLogSpotTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Spot entity.
/// </summary>
public class SpotConfiguration : IEntityTypeConfiguration<Spot>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Spot> builder)
    {
        builder.ToTable("Spots");

        builder.HasKey(x => x.SpotId);

        builder.Property(x => x.SpotId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.LocationType)
            .IsRequired();

        builder.Property(x => x.Latitude)
            .HasPrecision(9, 6);

        builder.Property(x => x.Longitude)
            .HasPrecision(9, 6);

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.WaterBodyName)
            .HasMaxLength(200);

        builder.Property(x => x.Directions)
            .HasMaxLength(1000);

        builder.Property(x => x.IsFavorite)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.Trips)
            .WithOne(x => x.Spot)
            .HasForeignKey(x => x.SpotId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => new { x.UserId, x.IsFavorite });
    }
}
