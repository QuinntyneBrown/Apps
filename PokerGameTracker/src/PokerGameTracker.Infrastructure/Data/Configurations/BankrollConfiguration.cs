// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokerGameTracker.Core;

namespace PokerGameTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Bankroll entity.
/// </summary>
public class BankrollConfiguration : IEntityTypeConfiguration<Bankroll>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Bankroll> builder)
    {
        builder.ToTable("Bankrolls");

        builder.HasKey(x => x.BankrollId);

        builder.Property(x => x.BankrollId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Amount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.RecordedDate)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.RecordedDate);
    }
}
