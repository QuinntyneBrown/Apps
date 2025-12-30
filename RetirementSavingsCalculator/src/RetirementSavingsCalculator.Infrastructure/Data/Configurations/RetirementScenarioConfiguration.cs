// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RetirementSavingsCalculator.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RetirementSavingsCalculator.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the RetirementScenario entity.
/// </summary>
public class RetirementScenarioConfiguration : IEntityTypeConfiguration<RetirementScenario>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<RetirementScenario> builder)
    {
        builder.ToTable("RetirementScenarios");

        builder.HasKey(x => x.RetirementScenarioId);

        builder.Property(x => x.RetirementScenarioId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.CurrentAge)
            .IsRequired();

        builder.Property(x => x.RetirementAge)
            .IsRequired();

        builder.Property(x => x.LifeExpectancyAge)
            .IsRequired();

        builder.Property(x => x.CurrentSavings)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.AnnualContribution)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.ExpectedReturnRate)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.Property(x => x.InflationRate)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.Property(x => x.ProjectedAnnualIncome)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.ProjectedAnnualExpenses)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.LastUpdated)
            .IsRequired();

        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.CreatedAt);
    }
}
