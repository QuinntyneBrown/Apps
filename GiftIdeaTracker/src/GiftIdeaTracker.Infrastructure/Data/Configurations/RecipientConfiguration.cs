// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GiftIdeaTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GiftIdeaTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Recipient entity.
/// </summary>
public class RecipientConfiguration : IEntityTypeConfiguration<Recipient>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Recipient> builder)
    {
        builder.ToTable("Recipients");

        builder.HasKey(x => x.RecipientId);

        builder.Property(x => x.RecipientId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Relationship)
            .HasMaxLength(100);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Name);
    }
}
