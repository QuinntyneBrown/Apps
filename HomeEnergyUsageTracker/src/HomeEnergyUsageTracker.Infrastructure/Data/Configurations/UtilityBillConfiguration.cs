// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeEnergyUsageTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the UtilityBill entity.
/// </summary>
public class UtilityBillConfiguration : IEntityTypeConfiguration<UtilityBill>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<UtilityBill> builder)
    {
        builder.ToTable("UtilityBills");

        builder.HasKey(x => x.UtilityBillId);

        builder.Property(x => x.UtilityBillId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.UtilityType)
            .IsRequired();

        builder.Property(x => x.BillingDate)
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.UsageAmount)
            .HasPrecision(18, 2);

        builder.Property(x => x.Unit)
            .HasMaxLength(50);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.BillingDate);
        builder.HasIndex(x => new { x.UserId, x.UtilityType });
    }
}
