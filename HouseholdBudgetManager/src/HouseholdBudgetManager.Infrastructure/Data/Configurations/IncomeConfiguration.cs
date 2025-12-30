// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HouseholdBudgetManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseholdBudgetManager.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Income entity.
/// </summary>
public class IncomeConfiguration : IEntityTypeConfiguration<Income>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Income> builder)
    {
        builder.ToTable("Incomes");

        builder.HasKey(x => x.IncomeId);

        builder.Property(x => x.IncomeId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.BudgetId)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Source)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.IncomeDate)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.IsRecurring)
            .IsRequired();

        builder.HasIndex(x => x.BudgetId);
        builder.HasIndex(x => x.IncomeDate);
    }
}
