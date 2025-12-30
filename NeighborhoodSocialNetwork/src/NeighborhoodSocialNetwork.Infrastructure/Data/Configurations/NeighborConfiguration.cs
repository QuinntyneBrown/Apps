// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NeighborhoodSocialNetwork.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NeighborhoodSocialNetwork.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Neighbor entity.
/// </summary>
public class NeighborConfiguration : IEntityTypeConfiguration<Neighbor>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Neighbor> builder)
    {
        builder.ToTable("Neighbors");

        builder.HasKey(x => x.NeighborId);

        builder.Property(x => x.NeighborId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Address)
            .HasMaxLength(500);

        builder.Property(x => x.ContactInfo)
            .HasMaxLength(300);

        builder.Property(x => x.Bio)
            .HasMaxLength(2000);

        builder.Property(x => x.Interests)
            .HasMaxLength(1000);

        builder.Property(x => x.IsVerified)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.IsVerified);
    }
}
