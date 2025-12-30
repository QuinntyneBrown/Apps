// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PerformanceReviewPrepTool.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PerformanceReviewPrepTool.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Feedback entity.
/// </summary>
public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.ToTable("Feedbacks");

        builder.HasKey(x => x.FeedbackId);

        builder.Property(x => x.FeedbackId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ReviewPeriodId)
            .IsRequired();

        builder.Property(x => x.Source)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.ReceivedDate)
            .IsRequired();

        builder.Property(x => x.FeedbackType)
            .HasMaxLength(100);

        builder.Property(x => x.Category)
            .HasMaxLength(100);

        builder.Property(x => x.IsKeyFeedback)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ReviewPeriodId);
        builder.HasIndex(x => x.ReceivedDate);
        builder.HasIndex(x => x.IsKeyFeedback);
    }
}
