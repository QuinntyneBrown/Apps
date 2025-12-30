// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WarrantyReturnPeriodTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WarrantyReturnPeriodTracker.Infrastructure;

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

        builder.Property(x => x.PurchaseId)
            .IsRequired();

        builder.Property(x => x.ReceiptNumber)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.ReceiptType)
            .IsRequired();

        builder.Property(x => x.Format)
            .IsRequired();

        builder.Property(x => x.StorageLocation)
            .HasMaxLength(500);

        builder.Property(x => x.ReceiptDate)
            .IsRequired();

        builder.Property(x => x.StoreName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.TotalAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.PaymentMethod)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasDefaultValue(ReceiptStatus.Active);

        builder.Property(x => x.IsVerified)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.UploadedAt)
            .IsRequired();

        builder.HasIndex(x => x.PurchaseId);
        builder.HasIndex(x => x.ReceiptNumber);
        builder.HasIndex(x => x.Status);
    }
}
