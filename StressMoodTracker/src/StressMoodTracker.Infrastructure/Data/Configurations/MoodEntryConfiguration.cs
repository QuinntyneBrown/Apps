// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using StressMoodTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StressMoodTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the MoodEntry entity.
/// </summary>
public class MoodEntryConfiguration : IEntityTypeConfiguration<MoodEntry>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<MoodEntry> builder)
    {
        builder.ToTable("MoodEntries");

        builder.HasKey(x => x.MoodEntryId);

        builder.Property(x => x.MoodEntryId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.MoodLevel)
            .IsRequired();

        builder.Property(x => x.StressLevel)
            .IsRequired();

        builder.Property(x => x.EntryTime)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.Activities)
            .HasMaxLength(500);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.EntryTime);
        builder.HasIndex(x => new { x.UserId, x.EntryTime });
    }
}
