// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SleepQualityTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SleepQualityTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Habit entity.
/// </summary>
public class HabitConfiguration : IEntityTypeConfiguration<Habit>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Habit> builder)
    {
        builder.ToTable("Habits");

        builder.HasKey(x => x.HabitId);

        builder.Property(x => x.HabitId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.HabitType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.IsPositive)
            .IsRequired();

        builder.Property(x => x.TypicalTime);

        builder.Property(x => x.ImpactLevel)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.HabitType);
        builder.HasIndex(x => x.IsActive);
    }
}
