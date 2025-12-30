// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeGymEquipmentManager.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the WorkoutMapping entity.
/// </summary>
public class WorkoutMappingConfiguration : IEntityTypeConfiguration<WorkoutMapping>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<WorkoutMapping> builder)
    {
        builder.ToTable("WorkoutMappings");

        builder.HasKey(x => x.WorkoutMappingId);

        builder.Property(x => x.WorkoutMappingId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.EquipmentId)
            .IsRequired();

        builder.Property(x => x.ExerciseName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.MuscleGroup)
            .HasMaxLength(100);

        builder.Property(x => x.Instructions)
            .HasMaxLength(1000);

        builder.Property(x => x.IsFavorite)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.EquipmentId);
        builder.HasIndex(x => new { x.UserId, x.IsFavorite });
    }
}
