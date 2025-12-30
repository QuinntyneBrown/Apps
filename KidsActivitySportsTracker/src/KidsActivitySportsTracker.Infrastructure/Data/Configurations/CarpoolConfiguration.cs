// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KidsActivitySportsTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KidsActivitySportsTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Carpool entity.
/// </summary>
public class CarpoolConfiguration : IEntityTypeConfiguration<Carpool>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Carpool> builder)
    {
        builder.ToTable("Carpools");

        builder.HasKey(x => x.CarpoolId);

        builder.Property(x => x.CarpoolId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.DriverName)
            .HasMaxLength(200);

        builder.Property(x => x.DriverContact)
            .HasMaxLength(200);

        builder.Property(x => x.PickupLocation)
            .HasMaxLength(300);

        builder.Property(x => x.DropoffLocation)
            .HasMaxLength(300);

        builder.Property(x => x.Participants)
            .HasMaxLength(500);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
    }
}
