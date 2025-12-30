// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HydrationTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HydrationTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Intake entity.
/// </summary>
public class IntakeConfiguration : IEntityTypeConfiguration<Intake>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Intake> builder)
    {
        builder.ToTable("Intakes");

        builder.HasKey(x => x.IntakeId);

        builder.Property(x => x.IntakeId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.BeverageType)
            .IsRequired();

        builder.Property(x => x.AmountMl)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.IntakeTime)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.IntakeTime);
        builder.HasIndex(x => x.BeverageType);
    }
}
