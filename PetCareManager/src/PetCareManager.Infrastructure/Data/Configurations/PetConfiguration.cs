// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PetCareManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetCareManager.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Pet entity.
/// </summary>
public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("Pets");

        builder.HasKey(x => x.PetId);

        builder.Property(x => x.PetId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.PetType)
            .IsRequired();

        builder.Property(x => x.Breed)
            .HasMaxLength(100);

        builder.Property(x => x.Color)
            .HasMaxLength(50);

        builder.Property(x => x.Weight)
            .HasPrecision(8, 2);

        builder.Property(x => x.MicrochipNumber)
            .HasMaxLength(20);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.VetAppointments)
            .WithOne(x => x.Pet)
            .HasForeignKey(x => x.PetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Medications)
            .WithOne(x => x.Pet)
            .HasForeignKey(x => x.PetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Vaccinations)
            .WithOne(x => x.Pet)
            .HasForeignKey(x => x.PetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.MicrochipNumber);
    }
}
