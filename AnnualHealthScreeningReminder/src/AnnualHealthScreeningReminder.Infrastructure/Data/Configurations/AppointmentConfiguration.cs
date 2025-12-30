// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnnualHealthScreeningReminder.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Appointment entity.
/// </summary>
public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("Appointments");

        builder.HasKey(x => x.AppointmentId);

        builder.Property(x => x.AppointmentId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ScreeningId)
            .IsRequired();

        builder.Property(x => x.AppointmentDate)
            .IsRequired();

        builder.Property(x => x.Location)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.Provider)
            .HasMaxLength(200);

        builder.Property(x => x.IsCompleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Ignore(x => x.IsUpcoming());

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ScreeningId);
        builder.HasIndex(x => x.AppointmentDate);
        builder.HasIndex(x => new { x.UserId, x.IsCompleted });
    }
}
