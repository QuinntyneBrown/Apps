// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RoadsideAssistanceInfoHub.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RoadsideAssistanceInfoHub.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the EmergencyContact entity.
/// </summary>
public class EmergencyContactConfiguration : IEntityTypeConfiguration<EmergencyContact>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<EmergencyContact> builder)
    {
        builder.ToTable("EmergencyContacts");

        builder.HasKey(x => x.EmergencyContactId);

        builder.Property(x => x.EmergencyContactId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Relationship)
            .HasMaxLength(100);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.AlternatePhone)
            .HasMaxLength(20);

        builder.Property(x => x.Email)
            .HasMaxLength(200);

        builder.Property(x => x.Address)
            .HasMaxLength(500);

        builder.Property(x => x.IsPrimaryContact)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.ContactType)
            .HasMaxLength(50);

        builder.Property(x => x.ServiceArea)
            .HasMaxLength(200);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.ContactType);
        builder.HasIndex(x => x.IsPrimaryContact);
    }
}
