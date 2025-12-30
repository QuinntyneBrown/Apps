// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoPortfolioManager.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the CryptoHolding entity.
/// </summary>
public class CryptoHoldingConfiguration : IEntityTypeConfiguration<CryptoHolding>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<CryptoHolding> builder)
    {
        builder.ToTable("CryptoHoldings");

        builder.HasKey(x => x.CryptoHoldingId);

        builder.Property(x => x.CryptoHoldingId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.WalletId)
            .IsRequired();

        builder.Property(x => x.Symbol)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Quantity)
            .IsRequired()
            .HasPrecision(18, 8);

        builder.Property(x => x.AverageCost)
            .IsRequired()
            .HasPrecision(18, 8);

        builder.Property(x => x.CurrentPrice)
            .IsRequired()
            .HasPrecision(18, 8);

        builder.Property(x => x.LastPriceUpdate)
            .IsRequired();

        builder.HasIndex(x => x.WalletId);
        builder.HasIndex(x => x.Symbol);
    }
}
