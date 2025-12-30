// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalLoanComparisonTool.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalLoanComparisonTool.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Loan entity.
/// </summary>
public class LoanConfiguration : IEntityTypeConfiguration<Loan>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.ToTable("Loans");

        builder.HasKey(x => x.LoanId);

        builder.Property(x => x.LoanId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.LoanType)
            .IsRequired();

        builder.Property(x => x.RequestedAmount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.Purpose)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.CreditScore)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.HasIndex(x => x.LoanType);
        builder.HasIndex(x => x.CreditScore);
    }
}
