// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SleepQualityTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SleepQualityTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the SleepSession entity.
/// </summary>
public class SleepSessionConfiguration : IEntityTypeConfiguration<SleepSession>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<SleepSession> builder)
    {
        builder.ToTable("SleepSessions");

        builder.HasKey(x => x.SleepSessionId);

        builder.Property(x => x.SleepSessionId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Bedtime)
            .IsRequired();

        builder.Property(x => x.WakeTime)
            .IsRequired();

        builder.Property(x => x.TotalSleepMinutes)
            .IsRequired();

        builder.Property(x => x.SleepQuality)
            .IsRequired();

        builder.Property(x => x.TimesAwakened);

        builder.Property(x => x.DeepSleepMinutes);

        builder.Property(x => x.RemSleepMinutes);

        builder.Property(x => x.SleepEfficiency)
            .HasPrecision(5, 2);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Bedtime);
        builder.HasIndex(x => x.SleepQuality);
    }
}
