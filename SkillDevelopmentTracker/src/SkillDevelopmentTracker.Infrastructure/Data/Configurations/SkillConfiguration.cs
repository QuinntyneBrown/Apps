// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SkillDevelopmentTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SkillDevelopmentTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Skill entity.
/// </summary>
public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable("Skills");

        builder.HasKey(x => x.SkillId);

        builder.Property(x => x.SkillId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Category)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.ProficiencyLevel)
            .IsRequired();

        builder.Property(x => x.TargetLevel);

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.TargetDate);

        builder.Property(x => x.HoursSpent)
            .HasPrecision(18, 2);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CourseIds)
            .HasConversion(
                v => string.Join(",", v.Select(g => g.ToString())),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                      .Select(s => Guid.Parse(s))
                      .ToList());

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Category);
        builder.HasIndex(x => x.ProficiencyLevel);
    }
}
