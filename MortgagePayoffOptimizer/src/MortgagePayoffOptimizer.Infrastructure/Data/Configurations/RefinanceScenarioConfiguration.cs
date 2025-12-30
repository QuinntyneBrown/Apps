// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MortgagePayoffOptimizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MortgagePayoffOptimizer.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the RefinanceScenario entity.
/// </summary>
public class RefinanceScenarioConfiguration : IEntityTypeConfiguration<RefinanceScenario>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<RefinanceScenario> builder)
    {
        builder.ToTable("RefinanceScenarios");

        builder.HasKey(x => x.RefinanceScenarioId);

        builder.Property(x => x.RefinanceScenarioId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.MortgageId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.NewInterestRate)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.Property(x => x.NewLoanTermYears)
            .IsRequired();

        builder.Property(x => x.RefinancingCosts)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.NewMonthlyPayment)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.MonthlySavings)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.BreakEvenMonths)
            .IsRequired();

        builder.Property(x => x.TotalSavings)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.Mortgage)
            .WithMany()
            .HasForeignKey(x => x.MortgageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.MortgageId);
        builder.HasIndex(x => x.CreatedAt);
        builder.HasIndex(x => new { x.MortgageId, x.Name });
    }
}
