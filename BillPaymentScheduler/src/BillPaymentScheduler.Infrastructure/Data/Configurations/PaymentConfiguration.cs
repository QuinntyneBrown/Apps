// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BillPaymentScheduler.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Payment entity.
/// </summary>
public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(x => x.PaymentId);

        builder.Property(x => x.PaymentId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.BillId)
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.PaymentDate)
            .IsRequired();

        builder.Property(x => x.ConfirmationNumber)
            .HasMaxLength(100);

        builder.Property(x => x.PaymentMethod)
            .HasMaxLength(100);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.HasOne(x => x.Bill)
            .WithMany()
            .HasForeignKey(x => x.BillId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.BillId);
        builder.HasIndex(x => x.PaymentDate);
    }
}
