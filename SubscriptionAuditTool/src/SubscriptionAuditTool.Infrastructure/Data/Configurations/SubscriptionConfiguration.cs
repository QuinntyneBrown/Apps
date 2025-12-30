// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SubscriptionAuditTool.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SubscriptionAuditTool.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Subscription entity.
/// </summary>
public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("Subscriptions");

        builder.HasKey(x => x.SubscriptionId);

        builder.Property(x => x.SubscriptionId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ServiceName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Cost)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.BillingCycle)
            .IsRequired();

        builder.Property(x => x.NextBillingDate)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.CancellationDate);

        builder.Property(x => x.CategoryId);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Subscriptions)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.ServiceName);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.NextBillingDate);
        builder.HasIndex(x => x.CategoryId);
    }
}
