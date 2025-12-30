// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ResumeCareerAchievementTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumeCareerAchievementTracker.Infrastructure;

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

        builder.Property(x => x.Title)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(x => x.AchievementType)
            .IsRequired();

        builder.Property(x => x.AchievedDate)
            .IsRequired();

        builder.Property(x => x.Organization)
            .HasMaxLength(200);

        builder.Property(x => x.Impact)
            .HasMaxLength(1000);

        builder.Property(x => x.SkillIds)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList())
            .HasMaxLength(2000);

        builder.Property(x => x.ProjectIds)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList())
            .HasMaxLength(2000);

        builder.Property(x => x.IsFeatured)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.Tags)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.AchievedDate);
        builder.HasIndex(x => new { x.UserId, x.IsFeatured });
    }
}
