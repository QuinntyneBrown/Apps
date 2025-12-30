// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MeetingNotesActionItemTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetingNotesActionItemTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Meeting entity.
/// </summary>
public class MeetingConfiguration : IEntityTypeConfiguration<Meeting>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Meeting> builder)
    {
        builder.ToTable("Meetings");

        builder.HasKey(x => x.MeetingId);

        builder.Property(x => x.MeetingId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.MeetingDateTime)
            .IsRequired();

        builder.Property(x => x.DurationMinutes);

        builder.Property(x => x.Location)
            .HasMaxLength(500);

        builder.Property(x => x.Attendees)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

        builder.Property(x => x.Agenda);

        builder.Property(x => x.Summary);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasMany(x => x.Notes)
            .WithOne(x => x.Meeting)
            .HasForeignKey(x => x.MeetingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.ActionItems)
            .WithOne(x => x.Meeting)
            .HasForeignKey(x => x.MeetingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.MeetingDateTime);
    }
}
