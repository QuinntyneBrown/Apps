// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CampingTripPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampingTripPlanner.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Campsite entity.
/// </summary>
public class CampsiteConfiguration : IEntityTypeConfiguration<Campsite>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Campsite> builder)
    {
        builder.ToTable("Campsites");

        builder.HasKey(x => x.CampsiteId);

        builder.Property(x => x.CampsiteId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Location)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.CampsiteType)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(2000);

        builder.Property(x => x.HasElectricity)
            .IsRequired();

        builder.Property(x => x.HasWater)
            .IsRequired();

        builder.Property(x => x.CostPerNight)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.IsFavorite)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Location);
        builder.HasIndex(x => x.CampsiteType);
        builder.HasIndex(x => x.IsFavorite);
    }
}
