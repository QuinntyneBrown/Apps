// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoPortfolioManager.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the TaxLot entity.
/// </summary>
public class TaxLotConfiguration : IEntityTypeConfiguration<TaxLot>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<TaxLot> builder)
    {
        builder.ToTable("TaxLots");

        builder.HasKey(x => x.TaxLotId);

        builder.Property(x => x.TaxLotId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.CryptoHoldingId)
            .IsRequired();

        builder.Property(x => x.AcquisitionDate)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .IsRequired()
            .HasPrecision(18, 8);

        builder.Property(x => x.CostBasis)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.IsDisposed)
            .IsRequired();

        builder.Property(x => x.DisposalDate);

        builder.Property(x => x.DisposalPrice)
            .HasPrecision(18, 8);

        builder.HasIndex(x => x.CryptoHoldingId);
        builder.HasIndex(x => x.AcquisitionDate);
        builder.HasIndex(x => x.IsDisposed);
    }
}
