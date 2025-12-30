// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WorkoutPlanBuilder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkoutPlanBuilder.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the ProgressRecord entity.
/// </summary>
public class ProgressRecordConfiguration : IEntityTypeConfiguration<ProgressRecord>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<ProgressRecord> builder)
    {
        builder.ToTable("ProgressRecords");

        builder.HasKey(x => x.ProgressRecordId);

        builder.Property(x => x.ProgressRecordId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.WorkoutId)
            .IsRequired();

        builder.Property(x => x.ActualDurationMinutes)
            .IsRequired();

        builder.Property(x => x.CaloriesBurned);

        builder.Property(x => x.PerformanceRating);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.ExerciseDetails)
            .HasMaxLength(4000);

        builder.Property(x => x.CompletedAt)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.WorkoutId);
        builder.HasIndex(x => x.CompletedAt);
        builder.HasIndex(x => x.PerformanceRating);
    }
}
