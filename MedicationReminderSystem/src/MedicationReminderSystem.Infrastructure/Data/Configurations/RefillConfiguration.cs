// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MedicationReminderSystem.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicationReminderSystem.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Refill entity.
/// </summary>
public class RefillConfiguration : IEntityTypeConfiguration<Refill>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Refill> builder)
    {
        builder.ToTable("Refills");

        builder.HasKey(x => x.RefillId);

        builder.Property(x => x.RefillId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.MedicationId)
            .IsRequired();

        builder.Property(x => x.RefillDate)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.Property(x => x.PharmacyName)
            .HasMaxLength(200);

        builder.Property(x => x.Cost)
            .HasPrecision(18, 2);

        builder.Property(x => x.NextRefillDate);

        builder.Property(x => x.RefillsRemaining);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.MedicationId);
        builder.HasIndex(x => x.NextRefillDate);
    }
}
