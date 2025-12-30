// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FinancialGoalTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialGoalTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Contribution entity.
/// </summary>
public class ContributionConfiguration : IEntityTypeConfiguration<Contribution>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Contribution> builder)
    {
        builder.ToTable("Contributions");

        builder.HasKey(x => x.ContributionId);

        builder.Property(x => x.ContributionId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.GoalId)
            .IsRequired();

        builder.Property(x => x.Amount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.ContributionDate)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(500);

        builder.HasOne(x => x.Goal)
            .WithMany()
            .HasForeignKey(x => x.GoalId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.GoalId);
        builder.HasIndex(x => x.ContributionDate);
    }
}
