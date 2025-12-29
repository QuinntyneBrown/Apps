// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnniversaryBirthdayReminder.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Gift entity.
/// </summary>
public class GiftConfiguration : IEntityTypeConfiguration<Gift>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Gift> builder)
    {
        builder.ToTable("Gifts");

        builder.HasKey(x => x.GiftId);

        builder.Property(x => x.GiftId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ImportantDateId)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.EstimatedPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.ActualPrice)
            .HasPrecision(18, 2);

        builder.Property(x => x.PurchaseUrl)
            .HasMaxLength(2000);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.PurchasedAt);

        builder.Property(x => x.DeliveredAt);

        builder.HasIndex(x => x.ImportantDateId);
        builder.HasIndex(x => x.Status);
    }
}
