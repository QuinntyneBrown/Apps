// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassicCarRestorationLog.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for the <see cref="Project"/> entity.
/// </summary>
public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    /// <summary>
    /// Configures the entity of type <see cref="Project"/>.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.ProjectId);

        builder.Property(p => p.CarMake)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.CarModel)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Phase)
            .IsRequired();

        builder.Property(p => p.StartDate)
            .IsRequired();

        builder.Property(p => p.Notes)
            .HasMaxLength(2000);

        builder.Property(p => p.EstimatedBudget)
            .HasPrecision(18, 2);

        builder.Property(p => p.ActualCost)
            .HasPrecision(18, 2);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.HasMany(p => p.Parts)
            .WithOne(pt => pt.Project)
            .HasForeignKey(pt => pt.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.WorkLogs)
            .WithOne(w => w.Project)
            .HasForeignKey(w => w.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.PhotoLogs)
            .WithOne(ph => ph.Project)
            .HasForeignKey(ph => ph.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
