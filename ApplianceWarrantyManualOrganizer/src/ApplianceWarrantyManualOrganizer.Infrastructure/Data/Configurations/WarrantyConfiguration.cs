// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplianceWarrantyManualOrganizer.Infrastructure;

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

        builder.Property(x => x.ApplianceId)
            .IsRequired();

        builder.Property(x => x.Provider)
            .HasMaxLength(200);

        builder.Property(x => x.CoverageDetails)
            .HasMaxLength(1000);

        builder.Property(x => x.DocumentUrl)
            .HasMaxLength(500);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.ApplianceId);
        builder.HasIndex(x => x.EndDate);
    }
}
