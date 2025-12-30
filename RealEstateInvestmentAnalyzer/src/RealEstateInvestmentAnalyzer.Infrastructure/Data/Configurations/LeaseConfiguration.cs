// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateInvestmentAnalyzer.Core;

namespace RealEstateInvestmentAnalyzer.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Lease entity.
/// </summary>
public class LeaseConfiguration : IEntityTypeConfiguration<Lease>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Lease> builder)
    {
        builder.ToTable("Leases");

        builder.HasKey(x => x.LeaseId);

        builder.Property(x => x.LeaseId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.PropertyId)
            .IsRequired();

        builder.Property(x => x.TenantName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.MonthlyRent)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.Property(x => x.SecurityDeposit)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.HasIndex(x => x.PropertyId);
        builder.HasIndex(x => x.IsActive);
        builder.HasIndex(x => x.StartDate);
        builder.HasIndex(x => x.EndDate);

        builder.HasOne(x => x.Property)
            .WithMany()
            .HasForeignKey(x => x.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
