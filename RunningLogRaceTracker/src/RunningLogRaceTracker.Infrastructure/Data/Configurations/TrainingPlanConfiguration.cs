// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RunningLogRaceTracker.Core;

namespace RunningLogRaceTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the TrainingPlan entity.
/// </summary>
public class TrainingPlanConfiguration : IEntityTypeConfiguration<TrainingPlan>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<TrainingPlan> builder)
    {
        builder.ToTable("TrainingPlans");

        builder.HasKey(x => x.TrainingPlanId);

        builder.Property(x => x.TrainingPlanId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.Property(x => x.WeeklyMileageGoal)
            .HasPrecision(10, 2);

        builder.Property(x => x.PlanDetails)
            .HasMaxLength(4000);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.Race)
            .WithMany()
            .HasForeignKey(x => x.RaceId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.RaceId);
        builder.HasIndex(x => new { x.UserId, x.IsActive });
    }
}
