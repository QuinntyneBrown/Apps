// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GiftIdeaTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GiftIdeaTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the GiftIdea entity.
/// </summary>
public class GiftIdeaConfiguration : IEntityTypeConfiguration<GiftIdea>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<GiftIdea> builder)
    {
        builder.ToTable("GiftIdeas");

        builder.HasKey(x => x.GiftIdeaId);

        builder.Property(x => x.GiftIdeaId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Occasion)
            .IsRequired();

        builder.Property(x => x.EstimatedPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.IsPurchased)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.RecipientId);
        builder.HasIndex(x => x.Occasion);
        builder.HasIndex(x => x.IsPurchased);
    }
}
