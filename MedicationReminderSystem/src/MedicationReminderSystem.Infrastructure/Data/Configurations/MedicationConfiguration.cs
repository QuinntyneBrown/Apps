// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MedicationReminderSystem.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicationReminderSystem.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Medication entity.
/// </summary>
public class MedicationConfiguration : IEntityTypeConfiguration<Medication>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Medication> builder)
    {
        builder.ToTable("Medications");

        builder.HasKey(x => x.MedicationId);

        builder.Property(x => x.MedicationId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.MedicationType)
            .IsRequired();

        builder.Property(x => x.Dosage)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.PrescribingDoctor)
            .HasMaxLength(200);

        builder.Property(x => x.PrescriptionDate);

        builder.Property(x => x.Purpose)
            .HasMaxLength(500);

        builder.Property(x => x.Instructions)
            .HasMaxLength(1000);

        builder.Property(x => x.SideEffects)
            .HasMaxLength(1000);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.DoseSchedules)
            .WithOne(x => x.Medication)
            .HasForeignKey(x => x.MedicationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Refills)
            .WithOne(x => x.Medication)
            .HasForeignKey(x => x.MedicationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => new { x.UserId, x.IsActive });
    }
}
