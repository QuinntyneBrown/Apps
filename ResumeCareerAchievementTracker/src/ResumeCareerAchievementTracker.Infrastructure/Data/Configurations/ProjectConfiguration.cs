// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ResumeCareerAchievementTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumeCareerAchievementTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Project entity.
/// </summary>
public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");

        builder.HasKey(x => x.ProjectId);

        builder.Property(x => x.ProjectId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(x => x.Organization)
            .HasMaxLength(200);

        builder.Property(x => x.Role)
            .HasMaxLength(200);

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate);

        builder.Property(x => x.Technologies)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
            .HasMaxLength(2000);

        builder.Property(x => x.Outcomes)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
            .HasMaxLength(3000);

        builder.Property(x => x.ProjectUrl)
            .HasMaxLength(500);

        builder.Property(x => x.IsFeatured)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.StartDate);
        builder.HasIndex(x => new { x.UserId, x.IsFeatured });
    }
}
