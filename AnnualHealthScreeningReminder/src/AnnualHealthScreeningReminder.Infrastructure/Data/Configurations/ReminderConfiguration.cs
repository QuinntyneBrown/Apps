// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnnualHealthScreeningReminder.Infrastructure;

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

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ScreeningId)
            .IsRequired();

        builder.Property(x => x.ReminderDate)
            .IsRequired();

        builder.Property(x => x.Message)
            .HasMaxLength(500);

        builder.Property(x => x.IsSent)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Ignore(x => x.MarkAsSent());

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ScreeningId);
        builder.HasIndex(x => x.ReminderDate);
        builder.HasIndex(x => new { x.UserId, x.IsSent });
    }
}
