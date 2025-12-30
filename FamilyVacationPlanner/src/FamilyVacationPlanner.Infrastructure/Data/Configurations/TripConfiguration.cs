// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyVacationPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyVacationPlanner.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Trip entity.
/// </summary>
public class TripConfiguration : IEntityTypeConfiguration<Trip>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.ToTable("Trips");

        builder.HasKey(x => x.TripId);

        builder.Property(x => x.TripId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Destination)
            .HasMaxLength(500);

        builder.Property(x => x.StartDate);

        builder.Property(x => x.EndDate);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.Itineraries)
            .WithOne(x => x.Trip)
            .HasForeignKey(x => x.TripId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Bookings)
            .WithOne(x => x.Trip)
            .HasForeignKey(x => x.TripId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Budgets)
            .WithOne(x => x.Trip)
            .HasForeignKey(x => x.TripId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.PackingLists)
            .WithOne(x => x.Trip)
            .HasForeignKey(x => x.TripId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.StartDate);
        builder.HasIndex(x => x.EndDate);
    }
}
