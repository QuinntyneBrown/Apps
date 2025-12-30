// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CharitableGivingTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Donation entity.
/// </summary>
public class DonationConfiguration : IEntityTypeConfiguration<Donation>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Donation> builder)
    {
        builder.ToTable("Donations");

        builder.HasKey(x => x.DonationId);

        builder.Property(x => x.DonationId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.OrganizationId)
            .IsRequired();

        builder.Property(x => x.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.DonationDate)
            .IsRequired();

        builder.Property(x => x.DonationType)
            .IsRequired();

        builder.Property(x => x.ReceiptNumber)
            .HasMaxLength(100);

        builder.Property(x => x.IsTaxDeductible)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.HasIndex(x => x.OrganizationId);
        builder.HasIndex(x => x.DonationDate);
        builder.HasIndex(x => x.IsTaxDeductible);
    }
}
