// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusSessionTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the SessionAnalytics entity.
/// </summary>
public class SessionAnalyticsConfiguration : IEntityTypeConfiguration<SessionAnalytics>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<SessionAnalytics> builder)
    {
        builder.ToTable("Analytics");

        builder.HasKey(x => x.SessionAnalyticsId);

        builder.Property(x => x.SessionAnalyticsId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.PeriodStartDate)
            .IsRequired();

        builder.Property(x => x.PeriodEndDate)
            .IsRequired();

        builder.Property(x => x.TotalSessions)
            .IsRequired();

        builder.Property(x => x.TotalFocusMinutes)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(x => x.AverageFocusScore)
            .HasPrecision(4, 2);

        builder.Property(x => x.TotalDistractions)
            .IsRequired();

        builder.Property(x => x.CompletionRate)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.Property(x => x.MostProductiveSessionType);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => new { x.UserId, x.PeriodStartDate, x.PeriodEndDate });
    }
}
