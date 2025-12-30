// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateInvestmentAnalyzer.Core;

namespace RealEstateInvestmentAnalyzer.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the CashFlow entity.
/// </summary>
public class CashFlowConfiguration : IEntityTypeConfiguration<CashFlow>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<CashFlow> builder)
    {
        builder.ToTable("CashFlows");

        builder.HasKey(x => x.CashFlowId);

        builder.Property(x => x.CashFlowId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.PropertyId)
            .IsRequired();

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.Income)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.Expenses)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.NetCashFlow)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.HasIndex(x => x.PropertyId);
        builder.HasIndex(x => x.Date);

        builder.HasOne(x => x.Property)
            .WithMany()
            .HasForeignKey(x => x.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
