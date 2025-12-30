// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MensGroupDiscussionTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MensGroupDiscussionTracker.Infrastructure;

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

        builder.Property(x => x.GroupId)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.MeetingDateTime)
            .IsRequired();

        builder.Property(x => x.Location)
            .HasMaxLength(500);

        builder.Property(x => x.Notes);

        builder.Property(x => x.AttendeeCount)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasMany(x => x.Topics)
            .WithOne(x => x.Meeting)
            .HasForeignKey(x => x.MeetingId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.GroupId);
        builder.HasIndex(x => x.MeetingDateTime);
    }
}
