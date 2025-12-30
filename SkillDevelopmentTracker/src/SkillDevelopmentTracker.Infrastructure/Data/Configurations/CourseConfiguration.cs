// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SkillDevelopmentTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SkillDevelopmentTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Course entity.
/// </summary>
public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");

        builder.HasKey(x => x.CourseId);

        builder.Property(x => x.CourseId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.Provider)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Instructor)
            .HasMaxLength(200);

        builder.Property(x => x.CourseUrl)
            .HasMaxLength(500);

        builder.Property(x => x.StartDate);

        builder.Property(x => x.CompletionDate);

        builder.Property(x => x.ProgressPercentage)
            .IsRequired();

        builder.Property(x => x.EstimatedHours)
            .HasPrecision(18, 2);

        builder.Property(x => x.ActualHours)
            .HasPrecision(18, 2);

        builder.Property(x => x.IsCompleted)
            .IsRequired();

        builder.Property(x => x.SkillIds)
            .HasConversion(
                v => string.Join(",", v.Select(g => g.ToString())),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                      .Select(s => Guid.Parse(s))
                      .ToList());

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Provider);
        builder.HasIndex(x => x.IsCompleted);
    }
}
