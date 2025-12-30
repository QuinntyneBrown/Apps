// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the WeeklyReview entity.
/// </summary>
public class WeeklyReviewConfiguration : IEntityTypeConfiguration<WeeklyReview>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<WeeklyReview> builder)
    {
        builder.ToTable("WeeklyReviews");

        builder.HasKey(x => x.WeeklyReviewId);

        builder.Property(x => x.WeeklyReviewId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.WeekStartDate)
            .IsRequired();

        builder.Property(x => x.WeekEndDate)
            .IsRequired();

        builder.Property(x => x.Reflections)
            .HasMaxLength(2000);

        builder.Property(x => x.LessonsLearned)
            .HasMaxLength(2000);

        builder.Property(x => x.Gratitude)
            .HasMaxLength(1000);

        builder.Property(x => x.ImprovementAreas)
            .HasMaxLength(1000);

        builder.Property(x => x.IsCompleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.Accomplishments)
            .WithOne(x => x.Review)
            .HasForeignKey(x => x.WeeklyReviewId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Challenges)
            .WithOne(x => x.Review)
            .HasForeignKey(x => x.WeeklyReviewId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Priorities)
            .WithOne(x => x.Review)
            .HasForeignKey(x => x.WeeklyReviewId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.WeekStartDate);
        builder.HasIndex(x => new { x.UserId, x.IsCompleted });
    }
}
