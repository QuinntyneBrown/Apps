// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeSavingsPlanner.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for the <see cref="Projection"/> entity.
/// </summary>
public class ProjectionConfiguration : IEntityTypeConfiguration<Projection>
{
    /// <summary>
    /// Configures the entity of type <see cref="Projection"/>.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<Projection> builder)
    {
        builder.HasKey(p => p.ProjectionId);

        builder.Property(p => p.PlanId)
            .IsRequired();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.CurrentSavings)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(p => p.MonthlyContribution)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(p => p.ExpectedReturnRate)
            .IsRequired()
            .HasPrecision(5, 2);

        builder.Property(p => p.YearsUntilCollege)
            .IsRequired();

        builder.Property(p => p.TargetGoal)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(p => p.ProjectedBalance)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.HasOne(p => p.Plan)
            .WithMany()
            .HasForeignKey(p => p.PlanId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
