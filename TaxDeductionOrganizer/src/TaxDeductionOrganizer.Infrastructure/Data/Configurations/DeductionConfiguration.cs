// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using TaxDeductionOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaxDeductionOrganizer.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Deduction entity.
/// </summary>
public class DeductionConfiguration : IEntityTypeConfiguration<Deduction>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Deduction> builder)
    {
        builder.ToTable("Deductions");

        builder.HasKey(x => x.DeductionId);

        builder.Property(x => x.DeductionId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.TaxYearId)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.Category)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.HasReceipt)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne(x => x.TaxYear)
            .WithMany(x => x.Deductions)
            .HasForeignKey(x => x.TaxYearId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.TaxYearId);
        builder.HasIndex(x => x.Date);
        builder.HasIndex(x => x.Category);
        builder.HasIndex(x => new { x.TaxYearId, x.Category });
    }
}
