// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InjuryPreventionRecoveryTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InjuryPreventionRecoveryTracker.Infrastructure;

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

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.InjuryId)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.Description)
            .HasMaxLength(2000);

        builder.Property(x => x.TargetDate);

        builder.Property(x => x.AchievedDate);

        builder.Property(x => x.IsAchieved)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.InjuryId);
        builder.HasIndex(x => x.IsAchieved);
        builder.HasIndex(x => x.TargetDate);
    }
}
