// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InjuryPreventionRecoveryTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InjuryPreventionRecoveryTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the RecoveryExercise entity.
/// </summary>
public class RecoveryExerciseConfiguration : IEntityTypeConfiguration<RecoveryExercise>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<RecoveryExercise> builder)
    {
        builder.ToTable("RecoveryExercises");

        builder.HasKey(x => x.RecoveryExerciseId);

        builder.Property(x => x.RecoveryExerciseId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.InjuryId)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.Description)
            .HasMaxLength(2000);

        builder.Property(x => x.Frequency)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.SetsAndReps)
            .HasMaxLength(200);

        builder.Property(x => x.DurationMinutes);

        builder.Property(x => x.Instructions)
            .HasMaxLength(2000);

        builder.Property(x => x.LastCompleted);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.InjuryId);
        builder.HasIndex(x => x.IsActive);
    }
}
