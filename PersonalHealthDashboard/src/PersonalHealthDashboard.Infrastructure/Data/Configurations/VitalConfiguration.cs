// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalHealthDashboard.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalHealthDashboard.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Vital entity.
/// </summary>
public class VitalConfiguration : IEntityTypeConfiguration<Vital>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Vital> builder)
    {
        builder.ToTable("Vitals");

        builder.HasKey(x => x.VitalId);

        builder.Property(x => x.VitalId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.VitalType)
            .IsRequired();

        builder.Property(x => x.Value)
            .IsRequired();

        builder.Property(x => x.Unit)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.MeasuredAt)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.Source)
            .HasMaxLength(200);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.VitalType);
        builder.HasIndex(x => x.MeasuredAt);
    }
}
