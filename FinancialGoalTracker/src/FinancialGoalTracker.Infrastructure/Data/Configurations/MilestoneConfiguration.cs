// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FinancialGoalTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialGoalTracker.Infrastructure;

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

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.TargetAmount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.TargetDate)
            .IsRequired();

        builder.Property(x => x.IsCompleted)
            .IsRequired();

        builder.Property(x => x.CompletedDate);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.HasIndex(x => x.GoalId);
        builder.HasIndex(x => x.IsCompleted);
        builder.HasIndex(x => x.TargetDate);
    }
}
