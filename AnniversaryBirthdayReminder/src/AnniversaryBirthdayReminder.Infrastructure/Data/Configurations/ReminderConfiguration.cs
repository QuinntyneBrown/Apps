// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnniversaryBirthdayReminder.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Reminder entity.
/// </summary>
public class ReminderConfiguration : IEntityTypeConfiguration<Reminder>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Reminder> builder)
    {
        builder.ToTable("Reminders");

        builder.HasKey(x => x.ReminderId);

        builder.Property(x => x.ReminderId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ImportantDateId)
            .IsRequired();

        builder.Property(x => x.ScheduledTime)
            .IsRequired();

        builder.Property(x => x.AdvanceNoticeDays)
            .IsRequired();

        builder.Property(x => x.DeliveryChannel)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.SentAt);

        builder.HasIndex(x => x.ImportantDateId);
        builder.HasIndex(x => x.ScheduledTime);
        builder.HasIndex(x => x.Status);
    }
}
