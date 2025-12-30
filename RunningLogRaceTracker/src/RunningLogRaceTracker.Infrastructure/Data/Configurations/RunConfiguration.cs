// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RunningLogRaceTracker.Core;

namespace RunningLogRaceTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Run entity.
/// </summary>
public class RunConfiguration : IEntityTypeConfiguration<Run>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Run> builder)
    {
        builder.ToTable("Runs");

        builder.HasKey(x => x.RunId);

        builder.Property(x => x.RunId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Distance)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(x => x.DurationMinutes)
            .IsRequired();

        builder.Property(x => x.CompletedAt)
            .IsRequired();

        builder.Property(x => x.AveragePace)
            .HasPrecision(10, 2);

        builder.Property(x => x.Route)
            .HasMaxLength(200);

        builder.Property(x => x.Weather)
            .HasMaxLength(100);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.CompletedAt);
    }
}
