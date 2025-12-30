// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PetCareManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetCareManager.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Vaccination entity.
/// </summary>
public class VaccinationConfiguration : IEntityTypeConfiguration<Vaccination>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Vaccination> builder)
    {
        builder.ToTable("Vaccinations");

        builder.HasKey(x => x.VaccinationId);

        builder.Property(x => x.VaccinationId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.PetId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.DateAdministered)
            .IsRequired();

        builder.Property(x => x.VetName)
            .HasMaxLength(200);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.PetId);
        builder.HasIndex(x => x.NextDueDate);
    }
}
