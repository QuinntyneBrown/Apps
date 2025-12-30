// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HabitFormationApp.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HabitFormationApp.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Streak entity.
/// </summary>
public class StreakConfiguration : IEntityTypeConfiguration<Streak>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Streak> builder)
    {
        builder.ToTable("Streaks");

        builder.HasKey(x => x.StreakId);

        builder.Property(x => x.StreakId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.HabitId)
            .IsRequired();

        builder.Property(x => x.CurrentStreak)
            .IsRequired();

        builder.Property(x => x.LongestStreak)
            .IsRequired();

        builder.Property(x => x.LastCompletedDate);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.HabitId);
        builder.HasIndex(x => x.CurrentStreak);
        builder.HasIndex(x => x.LongestStreak);
    }
}
