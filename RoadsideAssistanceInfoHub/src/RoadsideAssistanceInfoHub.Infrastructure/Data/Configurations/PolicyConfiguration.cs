// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RoadsideAssistanceInfoHub.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RoadsideAssistanceInfoHub.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Policy entity.
/// </summary>
public class PolicyConfiguration : IEntityTypeConfiguration<Policy>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Policy> builder)
    {
        builder.ToTable("Policies");

        builder.HasKey(x => x.PolicyId);

        builder.Property(x => x.PolicyId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.VehicleId)
            .IsRequired();

        builder.Property(x => x.Provider)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.PolicyNumber)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.Property(x => x.EmergencyPhone)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.MaxTowingDistance);

        builder.Property(x => x.ServiceCallsPerYear);

        builder.Property(x => x.CoveredServices)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
            .HasMaxLength(1000);

        builder.Property(x => x.AnnualPremium)
            .HasPrecision(18, 2);

        builder.Property(x => x.CoversBatteryService)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CoversFlatTire)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CoversFuelDelivery)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CoversLockout)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.HasIndex(x => x.VehicleId);
        builder.HasIndex(x => x.PolicyNumber);
        builder.HasIndex(x => x.EndDate);
    }
}
