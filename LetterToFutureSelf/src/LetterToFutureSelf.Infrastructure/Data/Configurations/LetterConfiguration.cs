// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using LetterToFutureSelf.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetterToFutureSelf.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Letter entity.
/// </summary>
public class LetterConfiguration : IEntityTypeConfiguration<Letter>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Letter> builder)
    {
        builder.ToTable("Letters");

        builder.HasKey(x => x.LetterId);

        builder.Property(x => x.LetterId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Subject)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Content)
            .IsRequired();

        builder.Property(x => x.WrittenDate)
            .IsRequired();

        builder.Property(x => x.ScheduledDeliveryDate)
            .IsRequired();

        builder.Property(x => x.DeliveryStatus)
            .IsRequired();

        builder.Property(x => x.HasBeenRead)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.DeliverySchedules)
            .WithOne(x => x.Letter)
            .HasForeignKey(x => x.LetterId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ScheduledDeliveryDate);
        builder.HasIndex(x => new { x.UserId, x.DeliveryStatus });
        builder.HasIndex(x => new { x.UserId, x.HasBeenRead });
    }
}
