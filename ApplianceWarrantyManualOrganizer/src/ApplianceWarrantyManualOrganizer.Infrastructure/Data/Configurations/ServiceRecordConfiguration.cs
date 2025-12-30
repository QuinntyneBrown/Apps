// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplianceWarrantyManualOrganizer.Infrastructure;

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

        builder.Property(x => x.ApplianceId)
            .IsRequired();

        builder.Property(x => x.ServiceDate)
            .IsRequired();

        builder.Property(x => x.ServiceProvider)
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.Cost)
            .HasPrecision(18, 2);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.ApplianceId);
        builder.HasIndex(x => x.ServiceDate);
    }
}
