// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoPortfolioManager.Infrastructure;

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

        builder.Property(x => x.WalletId)
            .IsRequired();

        builder.Property(x => x.TransactionDate)
            .IsRequired();

        builder.Property(x => x.TransactionType)
            .IsRequired();

        builder.Property(x => x.Symbol)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.Quantity)
            .IsRequired()
            .HasPrecision(18, 8);

        builder.Property(x => x.PricePerUnit)
            .IsRequired()
            .HasPrecision(18, 8);

        builder.Property(x => x.TotalAmount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.Fees)
            .HasPrecision(18, 2);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.HasIndex(x => x.WalletId);
        builder.HasIndex(x => x.TransactionDate);
        builder.HasIndex(x => x.TransactionType);
        builder.HasIndex(x => x.Symbol);
    }
}
