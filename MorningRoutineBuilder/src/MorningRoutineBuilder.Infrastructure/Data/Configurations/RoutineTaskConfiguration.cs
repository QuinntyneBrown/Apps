// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MorningRoutineBuilder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MorningRoutineBuilder.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the RoutineTask entity.
/// </summary>
public class RoutineTaskConfiguration : IEntityTypeConfiguration<RoutineTask>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<RoutineTask> builder)
    {
        builder.ToTable("RoutineTasks");

        builder.HasKey(x => x.RoutineTaskId);

        builder.Property(x => x.RoutineTaskId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.RoutineId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.TaskType)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.EstimatedDurationMinutes)
            .IsRequired();

        builder.Property(x => x.SortOrder)
            .IsRequired();

        builder.Property(x => x.IsOptional)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.RoutineId);
        builder.HasIndex(x => new { x.RoutineId, x.SortOrder });
    }
}
