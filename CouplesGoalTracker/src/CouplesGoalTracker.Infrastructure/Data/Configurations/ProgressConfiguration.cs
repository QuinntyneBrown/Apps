// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CouplesGoalTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Progress entity.
/// </summary>
public class ProgressConfiguration : IEntityTypeConfiguration<Progress>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Progress> builder)
    {
        builder.ToTable("Progresses");

        builder.HasKey(x => x.ProgressId);

        builder.Property(x => x.ProgressId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.GoalId)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ProgressDate)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CompletionPercentage)
            .IsRequired();

        builder.Property(x => x.EffortHours)
            .HasPrecision(10, 2);

        builder.Property(x => x.IsSignificant)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.GoalId);
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ProgressDate);
        builder.HasIndex(x => x.IsSignificant);
    }
}
