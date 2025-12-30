// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeMaintenanceSchedule.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeMaintenanceSchedule.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the ServiceLog entity.
/// </summary>
public class ServiceLogConfiguration : IEntityTypeConfiguration<ServiceLog>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<ServiceLog> builder)
    {
        builder.ToTable("ServiceLogs");

        builder.HasKey(x => x.ServiceLogId);

        builder.Property(x => x.ServiceLogId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.MaintenanceTaskId)
            .IsRequired();

        builder.Property(x => x.ServiceDate)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.Cost)
            .HasPrecision(18, 2);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.PartsUsed)
            .HasMaxLength(500);

        builder.Property(x => x.LaborHours)
            .HasPrecision(5, 2);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.Contractor)
            .WithMany(x => x.ServiceLogs)
            .HasForeignKey(x => x.ContractorId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.MaintenanceTaskId);
        builder.HasIndex(x => x.ContractorId);
        builder.HasIndex(x => x.ServiceDate);
    }
}
