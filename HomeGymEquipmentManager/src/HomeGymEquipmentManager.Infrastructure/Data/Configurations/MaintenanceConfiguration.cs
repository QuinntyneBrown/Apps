// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeGymEquipmentManager.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Maintenance entity.
/// </summary>
public class MaintenanceConfiguration : IEntityTypeConfiguration<Maintenance>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Maintenance> builder)
    {
        builder.ToTable("Maintenances");

        builder.HasKey(x => x.MaintenanceId);

        builder.Property(x => x.MaintenanceId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.EquipmentId)
            .IsRequired();

        builder.Property(x => x.MaintenanceDate)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Cost)
            .HasPrecision(18, 2);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.EquipmentId);
        builder.HasIndex(x => x.MaintenanceDate);
        builder.HasIndex(x => x.NextMaintenanceDate);
    }
}
