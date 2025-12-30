// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CharitableGivingTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the TaxReport entity.
/// </summary>
public class TaxReportConfiguration : IEntityTypeConfiguration<TaxReport>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<TaxReport> builder)
    {
        builder.ToTable("TaxReports");

        builder.HasKey(x => x.TaxReportId);

        builder.Property(x => x.TaxReportId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.TaxYear)
            .IsRequired();

        builder.Property(x => x.TotalCashDonations)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.TotalNonCashDonations)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.TotalDeductibleAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.GeneratedDate)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.HasIndex(x => x.TaxYear);
        builder.HasIndex(x => x.GeneratedDate);
    }
}
