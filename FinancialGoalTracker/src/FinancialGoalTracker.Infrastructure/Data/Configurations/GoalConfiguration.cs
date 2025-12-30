// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FinancialGoalTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialGoalTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Goal entity.
/// </summary>
public class GoalConfiguration : IEntityTypeConfiguration<Goal>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Goal> builder)
    {
        builder.ToTable("Goals");

        builder.HasKey(x => x.GoalId);

        builder.Property(x => x.GoalId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.GoalType)
            .IsRequired();

        builder.Property(x => x.TargetAmount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.CurrentAmount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.TargetDate)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.HasMany(x => x.Milestones)
            .WithOne(x => x.Goal)
            .HasForeignKey(x => x.GoalId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(x => x.CalculateProgress);

        builder.HasIndex(x => x.GoalType);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.TargetDate);
    }
}
