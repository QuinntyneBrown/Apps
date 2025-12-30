// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RetirementSavingsCalculator.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RetirementSavingsCalculator.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the WithdrawalStrategy entity.
/// </summary>
public class WithdrawalStrategyConfiguration : IEntityTypeConfiguration<WithdrawalStrategy>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<WithdrawalStrategy> builder)
    {
        builder.ToTable("WithdrawalStrategies");

        builder.HasKey(x => x.WithdrawalStrategyId);

        builder.Property(x => x.WithdrawalStrategyId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.RetirementScenarioId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.WithdrawalRate)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.Property(x => x.AnnualWithdrawalAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.AdjustForInflation)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.MinimumBalance)
            .HasPrecision(18, 2);

        builder.Property(x => x.StrategyType)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.HasOne(x => x.RetirementScenario)
            .WithMany()
            .HasForeignKey(x => x.RetirementScenarioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.RetirementScenarioId);
    }
}
