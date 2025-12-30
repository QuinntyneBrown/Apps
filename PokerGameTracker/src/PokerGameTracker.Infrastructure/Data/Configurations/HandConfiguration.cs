// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokerGameTracker.Core;

namespace PokerGameTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Hand entity.
/// </summary>
public class HandConfiguration : IEntityTypeConfiguration<Hand>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Hand> builder)
    {
        builder.ToTable("Hands");

        builder.HasKey(x => x.HandId);

        builder.Property(x => x.HandId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.SessionId)
            .IsRequired();

        builder.Property(x => x.StartingCards)
            .HasMaxLength(50);

        builder.Property(x => x.PotSize)
            .HasPrecision(18, 2);

        builder.Property(x => x.WasWon)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.SessionId);
        builder.HasIndex(x => x.WasWon);

        builder.HasOne(x => x.Session)
            .WithMany(x => x.Hands)
            .HasForeignKey(x => x.SessionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
