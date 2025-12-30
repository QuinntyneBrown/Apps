// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestmentPortfolioTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Dividend entity.
/// </summary>
public class DividendConfiguration : IEntityTypeConfiguration<Dividend>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Dividend> builder)
    {
        builder.ToTable("Dividends");

        builder.HasKey(x => x.DividendId);

        builder.Property(x => x.DividendId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.HoldingId)
            .IsRequired();

        builder.Property(x => x.PaymentDate)
            .IsRequired();

        builder.Property(x => x.ExDividendDate)
            .IsRequired();

        builder.Property(x => x.AmountPerShare)
            .HasPrecision(18, 6)
            .IsRequired();

        builder.Property(x => x.TotalAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.IsReinvested)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.HasIndex(x => x.HoldingId);
        builder.HasIndex(x => x.PaymentDate);
        builder.HasIndex(x => x.ExDividendDate);
    }
}
