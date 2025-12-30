// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalLegacyPlanner.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the TrustedContact entity.
/// </summary>
public class TrustedContactConfiguration : IEntityTypeConfiguration<TrustedContact>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<TrustedContact> builder)
    {
        builder.ToTable("TrustedContacts");

        builder.HasKey(x => x.TrustedContactId);

        builder.Property(x => x.TrustedContactId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.FullName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Relationship)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(50);

        builder.Property(x => x.Role)
            .HasMaxLength(200);

        builder.Property(x => x.IsPrimaryContact)
            .IsRequired();

        builder.Property(x => x.IsNotified)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.IsPrimaryContact);
        builder.HasIndex(x => x.Email);
    }
}
