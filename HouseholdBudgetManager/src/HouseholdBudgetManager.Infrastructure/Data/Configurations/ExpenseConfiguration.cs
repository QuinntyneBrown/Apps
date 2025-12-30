// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HouseholdBudgetManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseholdBudgetManager.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Expense entity.
/// </summary>
public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.ToTable("Expenses");

        builder.HasKey(x => x.ExpenseId);

        builder.Property(x => x.ExpenseId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.BudgetId)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Category)
            .IsRequired();

        builder.Property(x => x.ExpenseDate)
            .IsRequired();

        builder.Property(x => x.Payee)
            .HasMaxLength(200);

        builder.Property(x => x.PaymentMethod)
            .HasMaxLength(100);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.IsRecurring)
            .IsRequired();

        builder.HasIndex(x => x.BudgetId);
        builder.HasIndex(x => x.Category);
        builder.HasIndex(x => x.ExpenseDate);
    }
}
