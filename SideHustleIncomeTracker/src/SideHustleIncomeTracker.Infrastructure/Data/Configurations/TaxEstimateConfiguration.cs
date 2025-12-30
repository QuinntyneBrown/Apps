// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SideHustleIncomeTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SideHustleIncomeTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the TaxEstimate entity.
/// </summary>
public class TaxEstimateConfiguration : IEntityTypeConfiguration<TaxEstimate>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<TaxEstimate> builder)
    {
        builder.ToTable("TaxEstimates");

        builder.HasKey(x => x.TaxEstimateId);

        builder.Property(x => x.TaxEstimateId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.BusinessId)
            .IsRequired();

        builder.Property(x => x.TaxYear)
            .IsRequired();

        builder.Property(x => x.Quarter)
            .IsRequired();

        builder.Property(x => x.NetProfit)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.SelfEmploymentTax)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.IncomeTax)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.TotalEstimatedTax)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.IsPaid)
            .IsRequired();

        builder.Property(x => x.PaymentDate);

        builder.HasIndex(x => x.BusinessId);
        builder.HasIndex(x => new { x.TaxYear, x.Quarter });
        builder.HasIndex(x => x.IsPaid);
    }
}
