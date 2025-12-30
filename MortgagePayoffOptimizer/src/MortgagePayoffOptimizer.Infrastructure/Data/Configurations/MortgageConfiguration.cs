// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MortgagePayoffOptimizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MortgagePayoffOptimizer.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Mortgage entity.
/// </summary>
public class MortgageConfiguration : IEntityTypeConfiguration<Mortgage>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Mortgage> builder)
    {
        builder.ToTable("Mortgages");

        builder.HasKey(x => x.MortgageId);

        builder.Property(x => x.MortgageId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.PropertyAddress)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Lender)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.OriginalLoanAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.CurrentBalance)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.InterestRate)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.Property(x => x.LoanTermYears)
            .IsRequired();

        builder.Property(x => x.MonthlyPayment)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.MortgageType)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.HasIndex(x => x.PropertyAddress);
        builder.HasIndex(x => x.Lender);
        builder.HasIndex(x => x.IsActive);
    }
}
