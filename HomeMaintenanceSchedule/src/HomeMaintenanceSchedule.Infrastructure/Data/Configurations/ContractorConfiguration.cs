// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeMaintenanceSchedule.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeMaintenanceSchedule.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Contractor entity.
/// </summary>
public class ContractorConfiguration : IEntityTypeConfiguration<Contractor>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Contractor> builder)
    {
        builder.ToTable("Contractors");

        builder.HasKey(x => x.ContractorId);

        builder.Property(x => x.ContractorId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Specialty)
            .HasMaxLength(100);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(x => x.Email)
            .HasMaxLength(200);

        builder.Property(x => x.Website)
            .HasMaxLength(500);

        builder.Property(x => x.Address)
            .HasMaxLength(500);

        builder.Property(x => x.LicenseNumber)
            .HasMaxLength(100);

        builder.Property(x => x.IsInsured)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => new { x.UserId, x.IsActive });
    }
}
