// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MorningRoutineBuilder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MorningRoutineBuilder.Infrastructure;

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

        builder.Property(x => x.RoutineId)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.CurrentStreak)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.LongestStreak)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.LastCompletionDate);

        builder.Property(x => x.StreakStartDate);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.Routine)
            .WithMany()
            .HasForeignKey(x => x.RoutineId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.RoutineId);
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => new { x.UserId, x.IsActive });
    }
}
