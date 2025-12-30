// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WorkoutPlanBuilder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkoutPlanBuilder.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Workout entity.
/// </summary>
public class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Workout> builder)
    {
        builder.ToTable("Workouts");

        builder.HasKey(x => x.WorkoutId);

        builder.Property(x => x.WorkoutId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasMaxLength(2000);

        builder.Property(x => x.TargetDurationMinutes)
            .IsRequired();

        builder.Property(x => x.DifficultyLevel)
            .IsRequired();

        builder.Property(x => x.Goal)
            .HasMaxLength(100);

        builder.Property(x => x.IsTemplate)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.ScheduledDays)
            .HasMaxLength(500);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.IsActive);
        builder.HasIndex(x => x.IsTemplate);
        builder.HasIndex(x => x.DifficultyLevel);
    }
}
