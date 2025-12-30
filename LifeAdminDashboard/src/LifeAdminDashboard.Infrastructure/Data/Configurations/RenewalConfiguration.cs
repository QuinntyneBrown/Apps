// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using LifeAdminDashboard.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LifeAdminDashboard.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Renewal entity.
/// </summary>
public class RenewalConfiguration : IEntityTypeConfiguration<Renewal>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Renewal> builder)
    {
        builder.ToTable("Renewals");

        builder.HasKey(x => x.RenewalId);

        builder.Property(x => x.RenewalId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.RenewalType)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Provider)
            .HasMaxLength(200);

        builder.Property(x => x.RenewalDate)
            .IsRequired();

        builder.Property(x => x.Cost)
            .HasPrecision(18, 2);

        builder.Property(x => x.Frequency)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.IsAutoRenewal)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.RenewalDate);
        builder.HasIndex(x => new { x.UserId, x.IsActive });
        builder.HasIndex(x => new { x.UserId, x.RenewalType });
    }
}
