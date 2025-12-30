// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ResumeCareerAchievementTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumeCareerAchievementTracker.Infrastructure;

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
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Category)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.ProficiencyLevel)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.YearsOfExperience)
            .HasPrecision(5, 2);

        builder.Property(x => x.LastUsedDate);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.IsFeatured)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => new { x.UserId, x.Category });
        builder.HasIndex(x => new { x.UserId, x.IsFeatured });
    }
}
