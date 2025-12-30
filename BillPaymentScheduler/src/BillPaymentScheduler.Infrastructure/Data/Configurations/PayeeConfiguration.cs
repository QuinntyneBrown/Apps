// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BillPaymentScheduler.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Payee entity.
/// </summary>
public class PayeeConfiguration : IEntityTypeConfiguration<Payee>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Payee> builder)
    {
        builder.ToTable("Payees");

        builder.HasKey(x => x.PayeeId);

        builder.Property(x => x.PayeeId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.AccountNumber)
            .HasMaxLength(100);

        builder.Property(x => x.Website)
            .HasMaxLength(300);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(50);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.HasMany(x => x.Bills)
            .WithOne(x => x.Payee)
            .HasForeignKey(x => x.PayeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.Name);
    }
}
