// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PerformanceReviewPrepTool.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PerformanceReviewPrepTool.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Achievement entity.
/// </summary>
public class AchievementConfiguration : IEntityTypeConfiguration<Achievement>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Achievement> builder)
    {
        builder.ToTable("Achievements");

        builder.HasKey(x => x.AchievementId);

        builder.Property(x => x.AchievementId)
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

        builder.Property(x => x.AchievedDate)
            .IsRequired();

        builder.Property(x => x.Impact)
            .HasMaxLength(1000);

        builder.Property(x => x.Category)
            .HasMaxLength(100);

        builder.Property(x => x.IsKeyAchievement)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ReviewPeriodId);
        builder.HasIndex(x => x.AchievedDate);
        builder.HasIndex(x => x.IsKeyAchievement);
    }
}
