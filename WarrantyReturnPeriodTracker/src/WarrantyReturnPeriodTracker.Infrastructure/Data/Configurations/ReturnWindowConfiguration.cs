// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WarrantyReturnPeriodTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WarrantyReturnPeriodTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the ReturnWindow entity.
/// </summary>
public class ReturnWindowConfiguration : IEntityTypeConfiguration<ReturnWindow>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<ReturnWindow> builder)
    {
        builder.ToTable("ReturnWindows");

        builder.HasKey(x => x.ReturnWindowId);

        builder.Property(x => x.ReturnWindowId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.PurchaseId)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.Property(x => x.DurationDays)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasDefaultValue(ReturnWindowStatus.Open);

        builder.Property(x => x.PolicyDetails)
            .HasMaxLength(1000);

        builder.Property(x => x.Conditions)
            .HasMaxLength(1000);

        builder.Property(x => x.RestockingFeePercent)
            .HasPrecision(5, 2);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.HasIndex(x => x.PurchaseId);
        builder.HasIndex(x => x.EndDate);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => new { x.PurchaseId, x.Status });
    }
}
