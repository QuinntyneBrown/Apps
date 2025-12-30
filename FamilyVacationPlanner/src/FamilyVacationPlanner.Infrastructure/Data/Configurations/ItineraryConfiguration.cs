// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyVacationPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyVacationPlanner.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Itinerary entity.
/// </summary>
public class ItineraryConfiguration : IEntityTypeConfiguration<Itinerary>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Itinerary> builder)
    {
        builder.ToTable("Itineraries");

        builder.HasKey(x => x.ItineraryId);

        builder.Property(x => x.ItineraryId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.TripId)
            .IsRequired();

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.Activity)
            .HasMaxLength(500);

        builder.Property(x => x.Location)
            .HasMaxLength(500);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.TripId);
        builder.HasIndex(x => x.Date);
    }
}
