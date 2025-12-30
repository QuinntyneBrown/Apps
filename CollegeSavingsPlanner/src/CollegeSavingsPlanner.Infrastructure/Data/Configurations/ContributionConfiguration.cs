// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeSavingsPlanner.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for the <see cref="Contribution"/> entity.
/// </summary>
public class ContributionConfiguration : IEntityTypeConfiguration<Contribution>
{
    /// <summary>
    /// Configures the entity of type <see cref="Contribution"/>.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<Contribution> builder)
    {
        builder.HasKey(c => c.ContributionId);

        builder.Property(c => c.PlanId)
            .IsRequired();

        builder.Property(c => c.Amount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(c => c.ContributionDate)
            .IsRequired();

        builder.Property(c => c.Contributor)
            .HasMaxLength(200);

        builder.Property(c => c.Notes)
            .HasMaxLength(500);

        builder.Property(c => c.IsRecurring)
            .IsRequired();

        builder.HasOne(c => c.Plan)
            .WithMany()
            .HasForeignKey(c => c.PlanId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
