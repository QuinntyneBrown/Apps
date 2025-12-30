// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using TaxDeductionOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaxDeductionOrganizer.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the TaxYear entity.
/// </summary>
public class TaxYearConfiguration : IEntityTypeConfiguration<TaxYear>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<TaxYear> builder)
    {
        builder.ToTable("TaxYears");

        builder.HasKey(x => x.TaxYearId);

        builder.Property(x => x.TaxYearId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Year)
            .IsRequired();

        builder.Property(x => x.IsFiled)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.FilingDate);

        builder.Property(x => x.TotalDeductions)
            .HasPrecision(18, 2)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.HasMany(x => x.Deductions)
            .WithOne(x => x.TaxYear)
            .HasForeignKey(x => x.TaxYearId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.Year)
            .IsUnique();
    }
}
