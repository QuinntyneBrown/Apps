// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalLoanComparisonTool.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalLoanComparisonTool.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Offer entity.
/// </summary>
public class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.ToTable("Offers");

        builder.HasKey(x => x.OfferId);

        builder.Property(x => x.OfferId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.LoanId)
            .IsRequired();

        builder.Property(x => x.LenderName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.LoanAmount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.InterestRate)
            .IsRequired()
            .HasPrecision(5, 2);

        builder.Property(x => x.TermMonths)
            .IsRequired();

        builder.Property(x => x.MonthlyPayment)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.TotalCost)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.Fees)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.HasIndex(x => x.LoanId);
        builder.HasIndex(x => x.LenderName);
        builder.HasIndex(x => x.InterestRate);
        builder.HasIndex(x => x.TotalCost);
    }
}
