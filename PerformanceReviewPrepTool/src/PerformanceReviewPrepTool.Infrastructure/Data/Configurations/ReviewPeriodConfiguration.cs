// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PerformanceReviewPrepTool.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PerformanceReviewPrepTool.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the ReviewPeriod entity.
/// </summary>
public class ReviewPeriodConfiguration : IEntityTypeConfiguration<ReviewPeriod>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<ReviewPeriod> builder)
    {
        builder.ToTable("ReviewPeriods");

        builder.HasKey(x => x.ReviewPeriodId);

        builder.Property(x => x.ReviewPeriodId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.Property(x => x.ReviewDueDate);

        builder.Property(x => x.ReviewerName)
            .HasMaxLength(200);

        builder.Property(x => x.IsCompleted)
            .IsRequired();

        builder.Property(x => x.CompletedDate);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.StartDate);
        builder.HasIndex(x => x.EndDate);
        builder.HasIndex(x => x.IsCompleted);
    }
}
