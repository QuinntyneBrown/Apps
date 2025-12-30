// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KidsActivitySportsTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KidsActivitySportsTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Schedule entity.
/// </summary>
public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.ToTable("Schedules");

        builder.HasKey(x => x.ScheduleId);

        builder.Property(x => x.ScheduleId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ActivityId)
            .IsRequired();

        builder.Property(x => x.EventType)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.DateTime)
            .IsRequired();

        builder.Property(x => x.Location)
            .HasMaxLength(300);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.IsConfirmed)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.ActivityId);
        builder.HasIndex(x => x.DateTime);
        builder.HasIndex(x => new { x.ActivityId, x.DateTime });
    }
}
