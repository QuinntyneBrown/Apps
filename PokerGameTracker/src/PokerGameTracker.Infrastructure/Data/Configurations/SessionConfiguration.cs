// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokerGameTracker.Core;

namespace PokerGameTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Session entity.
/// </summary>
public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable("Sessions");

        builder.HasKey(x => x.SessionId);

        builder.Property(x => x.SessionId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.GameType)
            .IsRequired();

        builder.Property(x => x.StartTime)
            .IsRequired();

        builder.Property(x => x.BuyIn)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.CashOut)
            .HasPrecision(18, 2);

        builder.Property(x => x.Location)
            .HasMaxLength(200);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.GameType);
        builder.HasIndex(x => x.StartTime);
    }
}
