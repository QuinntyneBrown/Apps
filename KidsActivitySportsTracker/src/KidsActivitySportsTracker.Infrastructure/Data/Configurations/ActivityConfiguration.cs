// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KidsActivitySportsTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KidsActivitySportsTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Activity entity.
/// </summary>
public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.ToTable("Activities");

        builder.HasKey(x => x.ActivityId);

        builder.Property(x => x.ActivityId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ChildName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.ActivityType)
            .IsRequired();

        builder.Property(x => x.Organization)
            .HasMaxLength(300);

        builder.Property(x => x.CoachName)
            .HasMaxLength(200);

        builder.Property(x => x.CoachContact)
            .HasMaxLength(200);

        builder.Property(x => x.Season)
            .HasMaxLength(100);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.Schedules)
            .WithOne(x => x.Activity)
            .HasForeignKey(x => x.ActivityId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ChildName);
        builder.HasIndex(x => new { x.UserId, x.ChildName });
    }
}
