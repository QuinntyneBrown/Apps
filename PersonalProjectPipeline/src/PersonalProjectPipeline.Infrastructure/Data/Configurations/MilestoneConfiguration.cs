// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalProjectPipeline.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalProjectPipeline.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Milestone entity.
/// </summary>
public class MilestoneConfiguration : IEntityTypeConfiguration<Milestone>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Milestone> builder)
    {
        builder.ToTable("Milestones");

        builder.HasKey(x => x.MilestoneId);

        builder.Property(x => x.MilestoneId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ProjectId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.IsAchieved)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.Tasks)
            .WithOne(x => x.Milestone)
            .HasForeignKey(x => x.MilestoneId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.ProjectId);
        builder.HasIndex(x => new { x.ProjectId, x.IsAchieved });
    }
}
