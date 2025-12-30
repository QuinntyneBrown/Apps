// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RunningLogRaceTracker.Core;

namespace RunningLogRaceTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Race entity.
/// </summary>
public class RaceConfiguration : IEntityTypeConfiguration<Race>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Race> builder)
    {
        builder.ToTable("Races");

        builder.HasKey(x => x.RaceId);

        builder.Property(x => x.RaceId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.RaceType)
            .IsRequired();

        builder.Property(x => x.RaceDate)
            .IsRequired();

        builder.Property(x => x.Location)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.Distance)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.IsCompleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.RaceDate);
        builder.HasIndex(x => new { x.UserId, x.IsCompleted });
    }
}
