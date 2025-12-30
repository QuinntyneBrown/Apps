// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PetCareManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetCareManager.Infrastructure;

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

        builder.Property(x => x.PetId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Dosage)
            .HasMaxLength(200);

        builder.Property(x => x.Frequency)
            .HasMaxLength(100);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.PetId);
    }
}
