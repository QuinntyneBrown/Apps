// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestmentPortfolioTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Transaction entity.
/// </summary>
public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");

        builder.HasKey(x => x.TransactionId);

        builder.Property(x => x.TransactionId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.AccountId)
            .IsRequired();

        builder.Property(x => x.HoldingId);

        builder.Property(x => x.TransactionDate)
            .IsRequired();

        builder.Property(x => x.TransactionType)
            .IsRequired();

        builder.Property(x => x.Symbol)
            .HasMaxLength(20);

        builder.Property(x => x.Shares)
            .HasPrecision(18, 6);

        builder.Property(x => x.PricePerShare)
            .HasPrecision(18, 6);

        builder.Property(x => x.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Fees)
            .HasPrecision(18, 2);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.HasIndex(x => x.AccountId);
        builder.HasIndex(x => x.TransactionDate);
        builder.HasIndex(x => x.TransactionType);
        builder.HasIndex(x => x.Symbol);
    }
}
