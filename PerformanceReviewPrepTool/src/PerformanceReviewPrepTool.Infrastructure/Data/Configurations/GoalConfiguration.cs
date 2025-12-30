// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PerformanceReviewPrepTool.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PerformanceReviewPrepTool.Infrastructure;

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

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ReviewPeriodId)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.TargetDate);

        builder.Property(x => x.CompletedDate);

        builder.Property(x => x.ProgressPercentage)
            .IsRequired();

        builder.Property(x => x.SuccessMetrics)
            .HasMaxLength(1000);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ReviewPeriodId);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.TargetDate);
    }
}
