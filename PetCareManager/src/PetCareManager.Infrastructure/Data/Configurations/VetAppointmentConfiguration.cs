// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PetCareManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetCareManager.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the VetAppointment entity.
/// </summary>
public class VetAppointmentConfiguration : IEntityTypeConfiguration<VetAppointment>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<VetAppointment> builder)
    {
        builder.ToTable("VetAppointments");

        builder.HasKey(x => x.VetAppointmentId);

        builder.Property(x => x.VetAppointmentId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.PetId)
            .IsRequired();

        builder.Property(x => x.AppointmentDate)
            .IsRequired();

        builder.Property(x => x.VetName)
            .HasMaxLength(200);

        builder.Property(x => x.Reason)
            .HasMaxLength(500);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.Cost)
            .HasPrecision(10, 2);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.PetId);
        builder.HasIndex(x => x.AppointmentDate);
    }
}
