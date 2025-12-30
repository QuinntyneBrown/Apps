// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MedicationReminderSystem.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicationReminderSystem.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the DoseSchedule entity.
/// </summary>
public class DoseScheduleConfiguration : IEntityTypeConfiguration<DoseSchedule>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<DoseSchedule> builder)
    {
        builder.ToTable("DoseSchedules");

        builder.HasKey(x => x.DoseScheduleId);

        builder.Property(x => x.DoseScheduleId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.MedicationId)
            .IsRequired();

        builder.Property(x => x.ScheduledTime)
            .IsRequired();

        builder.Property(x => x.DaysOfWeek)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Frequency)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.ReminderEnabled)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.ReminderOffsetMinutes)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.LastTaken);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.MedicationId);
        builder.HasIndex(x => new { x.UserId, x.IsActive });
    }
}
