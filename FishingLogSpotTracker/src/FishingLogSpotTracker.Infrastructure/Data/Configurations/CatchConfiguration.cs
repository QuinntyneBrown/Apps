// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FishingLogSpotTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishingLogSpotTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Catch entity.
/// </summary>
public class CatchConfiguration : IEntityTypeConfiguration<Catch>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Catch> builder)
    {
        builder.ToTable("Catches");

        builder.HasKey(x => x.CatchId);

        builder.Property(x => x.CatchId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.TripId)
            .IsRequired();

        builder.Property(x => x.Species)
            .IsRequired();

        builder.Property(x => x.Length)
            .HasPrecision(5, 2);

        builder.Property(x => x.Weight)
            .HasPrecision(6, 2);

        builder.Property(x => x.CatchTime)
            .IsRequired();

        builder.Property(x => x.BaitUsed)
            .HasMaxLength(200);

        builder.Property(x => x.WasReleased)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.PhotoUrl)
            .HasMaxLength(500);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.TripId);
        builder.HasIndex(x => x.CatchTime);
        builder.HasIndex(x => new { x.UserId, x.Species });
    }
}
