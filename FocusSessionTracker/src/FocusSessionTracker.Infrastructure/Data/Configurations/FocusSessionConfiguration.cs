// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusSessionTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the FocusSession entity.
/// </summary>
public class FocusSessionConfiguration : IEntityTypeConfiguration<FocusSession>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<FocusSession> builder)
    {
        builder.ToTable("Sessions");

        builder.HasKey(x => x.FocusSessionId);

        builder.Property(x => x.FocusSessionId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.SessionType)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.PlannedDurationMinutes)
            .IsRequired();

        builder.Property(x => x.StartTime)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.FocusScore);

        builder.Property(x => x.IsCompleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.Distractions)
            .WithOne(x => x.Session)
            .HasForeignKey(x => x.FocusSessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.StartTime);
        builder.HasIndex(x => new { x.UserId, x.IsCompleted });
    }
}
