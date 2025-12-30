// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NeighborhoodSocialNetwork.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NeighborhoodSocialNetwork.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Event entity.
/// </summary>
public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("Events");

        builder.HasKey(x => x.EventId);

        builder.Property(x => x.EventId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.CreatedByNeighborId)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(x => x.EventDateTime)
            .IsRequired();

        builder.Property(x => x.Location)
            .HasMaxLength(500);

        builder.Property(x => x.IsPublic)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.CreatedByNeighborId);
        builder.HasIndex(x => x.EventDateTime);
        builder.HasIndex(x => x.IsPublic);
    }
}
