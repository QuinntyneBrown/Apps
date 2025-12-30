// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WorkoutPlanBuilder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkoutPlanBuilder.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Exercise entity.
/// </summary>
public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.ToTable("Exercises");

        builder.HasKey(x => x.ExerciseId);

        builder.Property(x => x.ExerciseId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasMaxLength(2000);

        builder.Property(x => x.ExerciseType)
            .IsRequired();

        builder.Property(x => x.PrimaryMuscleGroup)
            .IsRequired();

        builder.Property(x => x.SecondaryMuscleGroups)
            .HasMaxLength(500);

        builder.Property(x => x.Equipment)
            .HasMaxLength(200);

        builder.Property(x => x.DifficultyLevel)
            .IsRequired();

        builder.Property(x => x.VideoUrl)
            .HasMaxLength(500);

        builder.Property(x => x.IsCustom)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ExerciseType);
        builder.HasIndex(x => x.PrimaryMuscleGroup);
        builder.HasIndex(x => x.DifficultyLevel);
        builder.HasIndex(x => x.IsCustom);
    }
}
