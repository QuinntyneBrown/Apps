// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestmentPortfolioTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Holding entity.
/// </summary>
public class HoldingConfiguration : IEntityTypeConfiguration<Holding>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Holding> builder)
    {
        builder.ToTable("Holdings");

        builder.HasKey(x => x.HoldingId);

        builder.Property(x => x.HoldingId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.AccountId)
            .IsRequired();

        builder.Property(x => x.Symbol)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.Shares)
            .HasPrecision(18, 6)
            .IsRequired();

        builder.Property(x => x.AverageCost)
            .HasPrecision(18, 6)
            .IsRequired();

        builder.Property(x => x.CurrentPrice)
            .HasPrecision(18, 6)
            .IsRequired();

        builder.Property(x => x.LastPriceUpdate)
            .IsRequired();

        builder.HasIndex(x => x.AccountId);
        builder.HasIndex(x => x.Symbol);
    }
}
