// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalProjectPipeline.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalProjectPipeline.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the ProjectTask entity.
/// </summary>
public class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<ProjectTask> builder)
    {
        builder.ToTable("Tasks");

        builder.HasKey(x => x.ProjectTaskId);

        builder.Property(x => x.ProjectTaskId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ProjectId)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.IsCompleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.ProjectId);
        builder.HasIndex(x => x.MilestoneId);
        builder.HasIndex(x => new { x.ProjectId, x.IsCompleted });
    }
}
