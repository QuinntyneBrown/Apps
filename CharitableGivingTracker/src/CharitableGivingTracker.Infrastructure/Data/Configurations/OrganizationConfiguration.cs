// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CharitableGivingTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Organization entity.
/// </summary>
public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.ToTable("Organizations");

        builder.HasKey(x => x.OrganizationId);

        builder.Property(x => x.OrganizationId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.EIN)
            .HasMaxLength(20);

        builder.Property(x => x.Address)
            .HasMaxLength(1000);

        builder.Property(x => x.Website)
            .HasMaxLength(500);

        builder.Property(x => x.Is501c3)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.EIN);
        builder.HasIndex(x => x.Is501c3);
    }
}
