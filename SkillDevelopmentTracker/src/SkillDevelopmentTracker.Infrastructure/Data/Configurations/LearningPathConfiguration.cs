// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SkillDevelopmentTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SkillDevelopmentTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the LearningPath entity.
/// </summary>
public class LearningPathConfiguration : IEntityTypeConfiguration<LearningPath>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<LearningPath> builder)
    {
        builder.ToTable("LearningPaths");

        builder.HasKey(x => x.LearningPathId);

        builder.Property(x => x.LearningPathId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.TargetDate);

        builder.Property(x => x.CompletionDate);

        builder.Property(x => x.CourseIds)
            .HasConversion(
                v => string.Join(",", v.Select(g => g.ToString())),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                      .Select(s => Guid.Parse(s))
                      .ToList());

        builder.Property(x => x.SkillIds)
            .HasConversion(
                v => string.Join(",", v.Select(g => g.ToString())),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                      .Select(s => Guid.Parse(s))
                      .ToList());

        builder.Property(x => x.ProgressPercentage)
            .IsRequired();

        builder.Property(x => x.IsCompleted)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.IsCompleted);
    }
}
