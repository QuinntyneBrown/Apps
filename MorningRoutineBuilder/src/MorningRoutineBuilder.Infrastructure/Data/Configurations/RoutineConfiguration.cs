// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MorningRoutineBuilder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MorningRoutineBuilder.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Routine entity.
/// </summary>
public class RoutineConfiguration : IEntityTypeConfiguration<Routine>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Routine> builder)
    {
        builder.ToTable("Routines");

        builder.HasKey(x => x.RoutineId);

        builder.Property(x => x.RoutineId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.TargetStartTime)
            .IsRequired();

        builder.Property(x => x.EstimatedDurationMinutes)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.Tasks)
            .WithOne(x => x.Routine)
            .HasForeignKey(x => x.RoutineId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.CompletionLogs)
            .WithOne(x => x.Routine)
            .HasForeignKey(x => x.RoutineId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => new { x.UserId, x.IsActive });
    }
}
