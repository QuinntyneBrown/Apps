// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CampingTripPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampingTripPlanner.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the GearChecklist entity.
/// </summary>
public class GearChecklistConfiguration : IEntityTypeConfiguration<GearChecklist>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<GearChecklist> builder)
    {
        builder.ToTable("GearChecklists");

        builder.HasKey(x => x.GearChecklistId);

        builder.Property(x => x.GearChecklistId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.TripId)
            .IsRequired();

        builder.Property(x => x.ItemName)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.IsPacked)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.TripId);
        builder.HasIndex(x => x.IsPacked);
    }
}
