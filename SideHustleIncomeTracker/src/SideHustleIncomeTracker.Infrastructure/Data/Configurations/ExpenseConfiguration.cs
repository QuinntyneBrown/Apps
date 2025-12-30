// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SideHustleIncomeTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SideHustleIncomeTracker.Infrastructure;

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

        builder.Property(x => x.BusinessId)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Amount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.ExpenseDate)
            .IsRequired();

        builder.Property(x => x.Category)
            .HasMaxLength(100);

        builder.Property(x => x.Vendor)
            .HasMaxLength(200);

        builder.Property(x => x.IsTaxDeductible)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.HasIndex(x => x.BusinessId);
        builder.HasIndex(x => x.ExpenseDate);
        builder.HasIndex(x => x.Category);
        builder.HasIndex(x => x.IsTaxDeductible);
    }
}
