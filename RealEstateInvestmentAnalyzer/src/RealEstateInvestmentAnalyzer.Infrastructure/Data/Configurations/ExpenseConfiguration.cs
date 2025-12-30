// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateInvestmentAnalyzer.Core;

namespace RealEstateInvestmentAnalyzer.Infrastructure;

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

        builder.Property(x => x.PropertyId)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Amount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.Category)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.IsRecurring)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.HasIndex(x => x.PropertyId);
        builder.HasIndex(x => x.Date);
        builder.HasIndex(x => x.Category);
        builder.HasIndex(x => x.IsRecurring);

        builder.HasOne(x => x.Property)
            .WithMany()
            .HasForeignKey(x => x.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
