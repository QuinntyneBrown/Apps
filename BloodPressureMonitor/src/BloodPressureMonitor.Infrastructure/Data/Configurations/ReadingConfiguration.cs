// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BloodPressureMonitor.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodPressureMonitor.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Reading entity.
/// </summary>
public class ReadingConfiguration : IEntityTypeConfiguration<Reading>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Reading> builder)
    {
        builder.ToTable("Readings");

        builder.HasKey(x => x.ReadingId);

        builder.Property(x => x.ReadingId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Systolic)
            .IsRequired();

        builder.Property(x => x.Diastolic)
            .IsRequired();

        builder.Property(x => x.Category)
            .IsRequired();

        builder.Property(x => x.MeasuredAt)
            .IsRequired();

        builder.Property(x => x.Position)
            .HasMaxLength(50);

        builder.Property(x => x.Arm)
            .HasMaxLength(20);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Ignore(x => x.DetermineCategory());
        builder.Ignore(x => x.IsCritical());

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.MeasuredAt);
        builder.HasIndex(x => x.Category);
        builder.HasIndex(x => new { x.UserId, x.MeasuredAt });
    }
}
