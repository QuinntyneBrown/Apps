// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BillPaymentScheduler.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Bill entity.
/// </summary>
public class BillConfiguration : IEntityTypeConfiguration<Bill>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        builder.ToTable("Bills");

        builder.HasKey(x => x.BillId);

        builder.Property(x => x.BillId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.PayeeId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.DueDate)
            .IsRequired();

        builder.Property(x => x.BillingFrequency)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.IsAutoPay)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Ignore(x => x.MarkAsPaid());
        builder.Ignore(x => x.CalculateNextDueDate());

        builder.HasIndex(x => x.PayeeId);
        builder.HasIndex(x => x.DueDate);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => new { x.Status, x.DueDate });
    }
}
