// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeMaintenanceSchedule.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeMaintenanceSchedule.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the MaintenanceTask entity.
/// </summary>
public class MaintenanceTaskConfiguration : IEntityTypeConfiguration<MaintenanceTask>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<MaintenanceTask> builder)
    {
        builder.ToTable("MaintenanceTasks");

        builder.HasKey(x => x.MaintenanceTaskId);

        builder.Property(x => x.MaintenanceTaskId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.MaintenanceType)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.EstimatedCost)
            .HasPrecision(18, 2);

        builder.Property(x => x.ActualCost)
            .HasPrecision(18, 2);

        builder.Property(x => x.Priority)
            .IsRequired()
            .HasDefaultValue(3);

        builder.Property(x => x.Location)
            .HasMaxLength(200);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired();

        builder.HasMany(x => x.ServiceLogs)
            .WithOne(x => x.MaintenanceTask)
            .HasForeignKey(x => x.MaintenanceTaskId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Contractor)
            .WithMany(x => x.MaintenanceTasks)
            .HasForeignKey(x => x.ContractorId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.DueDate);
        builder.HasIndex(x => new { x.UserId, x.Status });
        builder.HasIndex(x => new { x.UserId, x.DueDate });
    }
}
