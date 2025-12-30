// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FriendGroupEventCoordinator.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the RSVP entity.
/// </summary>
public class RSVPConfiguration : IEntityTypeConfiguration<RSVP>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<RSVP> builder)
    {
        builder.ToTable("RSVPs");

        builder.HasKey(x => x.RSVPId);

        builder.Property(x => x.RSVPId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.EventId)
            .IsRequired();

        builder.Property(x => x.MemberId)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Response)
            .IsRequired();

        builder.Property(x => x.AdditionalGuests)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.Notes)
            .HasMaxLength(500);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.EventId);
        builder.HasIndex(x => x.MemberId);
        builder.HasIndex(x => new { x.EventId, x.MemberId })
            .IsUnique();
    }
}
