// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using VehicleMaintenanceLogger.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VehicleMaintenanceLogger.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the ServiceRecord entity.
/// </summary>
public class ServiceRecordConfiguration : IEntityTypeConfiguration<ServiceRecord>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<ServiceRecord> builder)
    {
        builder.ToTable("ServiceRecords");

        builder.HasKey(x => x.ServiceRecordId);

        builder.Property(x => x.ServiceRecordId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.VehicleId)
            .IsRequired();

        builder.Property(x => x.ServiceType)
            .IsRequired();

        builder.Property(x => x.ServiceDate)
            .IsRequired();

        builder.Property(x => x.MileageAtService)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Cost)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.ServiceProvider)
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.PartsReplaced)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
            .HasMaxLength(2000);

        builder.Property(x => x.InvoiceNumber)
            .HasMaxLength(100);

        builder.HasIndex(x => x.VehicleId);
        builder.HasIndex(x => x.ServiceDate);
        builder.HasIndex(x => new { x.VehicleId, x.ServiceType });
    }
}
