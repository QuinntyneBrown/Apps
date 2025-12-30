// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsTeamFollowingTracker.Core;

namespace SportsTeamFollowingTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Statistic entity.
/// </summary>
public class StatisticConfiguration : IEntityTypeConfiguration<Statistic>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Statistic> builder)
    {
        builder.ToTable("Statistics");

        builder.HasKey(x => x.StatisticId);

        builder.Property(x => x.StatisticId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.TeamId)
            .IsRequired();

        builder.Property(x => x.StatName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Value)
            .HasPrecision(18, 4)
            .IsRequired();

        builder.Property(x => x.RecordedDate)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.TeamId);
        builder.HasIndex(x => new { x.TeamId, x.StatName });
    }
}
