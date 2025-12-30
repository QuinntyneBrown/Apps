// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HouseholdBudgetManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseholdBudgetManager.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Budget entity.
/// </summary>
public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.ToTable("Budgets");

        builder.HasKey(x => x.BudgetId);

        builder.Property(x => x.BudgetId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Period)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.Property(x => x.TotalIncome)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.TotalExpenses)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.Period);
        builder.HasIndex(x => x.StartDate);
        builder.HasIndex(x => x.Status);
    }
}
