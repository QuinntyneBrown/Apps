// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CouplesGoalTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Milestone entity.
/// </summary>
public class MilestoneConfiguration : IEntityTypeConfiguration<Milestone>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Milestone> builder)
    {
        builder.ToTable("Milestones");

        builder.HasKey(x => x.MilestoneId);

        builder.Property(x => x.MilestoneId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.GoalId)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.TargetDate);

        builder.Property(x => x.CompletedDate);

        builder.Property(x => x.IsCompleted)
            .IsRequired();

        builder.Property(x => x.SortOrder)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.GoalId);
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.IsCompleted);
        builder.HasIndex(x => x.TargetDate);
    }
}
