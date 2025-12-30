// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyVacationPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyVacationPlanner.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the PackingList entity.
/// </summary>
public class PackingListConfiguration : IEntityTypeConfiguration<PackingList>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<PackingList> builder)
    {
        builder.ToTable("PackingLists");

        builder.HasKey(x => x.PackingListId);

        builder.Property(x => x.PackingListId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.TripId)
            .IsRequired();

        builder.Property(x => x.ItemName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.IsPacked)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.TripId);
        builder.HasIndex(x => x.IsPacked);
    }
}
