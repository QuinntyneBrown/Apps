// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RetirementSavingsCalculator.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RetirementSavingsCalculator.Infrastructure;

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

        builder.Property(x => x.RetirementScenarioId)
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.ContributionDate)
            .IsRequired();

        builder.Property(x => x.AccountName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.IsEmployerMatch)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.HasOne(x => x.RetirementScenario)
            .WithMany()
            .HasForeignKey(x => x.RetirementScenarioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.RetirementScenarioId);
        builder.HasIndex(x => x.ContributionDate);
    }
}
