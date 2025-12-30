// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using TaxDeductionOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaxDeductionOrganizer.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Receipt entity.
/// </summary>
public class ReceiptConfiguration : IEntityTypeConfiguration<Receipt>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Receipt> builder)
    {
        builder.ToTable("Receipts");

        builder.HasKey(x => x.ReceiptId);

        builder.Property(x => x.ReceiptId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.DeductionId)
            .IsRequired();

        builder.Property(x => x.FileName)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.FileUrl)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.UploadDate)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.HasOne(x => x.Deduction)
            .WithMany()
            .HasForeignKey(x => x.DeductionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.DeductionId);
        builder.HasIndex(x => x.UploadDate);
    }
}
