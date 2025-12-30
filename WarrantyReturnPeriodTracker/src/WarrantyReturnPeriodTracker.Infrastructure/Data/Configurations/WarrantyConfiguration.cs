// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WarrantyReturnPeriodTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WarrantyReturnPeriodTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Warranty entity.
/// </summary>
public class WarrantyConfiguration : IEntityTypeConfiguration<Warranty>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Warranty> builder)
    {
        builder.ToTable("Warranties");

        builder.HasKey(x => x.WarrantyId);

        builder.Property(x => x.WarrantyId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.PurchaseId)
            .IsRequired();

        builder.Property(x => x.WarrantyType)
            .IsRequired();

        builder.Property(x => x.Provider)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.Property(x => x.DurationMonths)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasDefaultValue(WarrantyStatus.Active);

        builder.Property(x => x.CoverageDetails)
            .HasMaxLength(1000);

        builder.Property(x => x.Terms)
            .HasMaxLength(2000);

        builder.Property(x => x.RegistrationNumber)
            .HasMaxLength(100);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.HasIndex(x => x.PurchaseId);
        builder.HasIndex(x => x.EndDate);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => new { x.PurchaseId, x.Status });
    }
}
