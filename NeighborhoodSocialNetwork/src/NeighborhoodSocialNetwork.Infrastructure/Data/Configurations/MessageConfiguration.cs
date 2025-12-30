// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NeighborhoodSocialNetwork.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NeighborhoodSocialNetwork.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Message entity.
/// </summary>
public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("Messages");

        builder.HasKey(x => x.MessageId);

        builder.Property(x => x.MessageId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.SenderNeighborId)
            .IsRequired();

        builder.Property(x => x.RecipientNeighborId)
            .IsRequired();

        builder.Property(x => x.Subject)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.Content)
            .HasMaxLength(5000)
            .IsRequired();

        builder.Property(x => x.IsRead)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.ReadAt);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.SenderNeighbor)
            .WithMany(x => x.SentMessages)
            .HasForeignKey(x => x.SenderNeighborId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.SenderNeighborId);
        builder.HasIndex(x => x.RecipientNeighborId);
        builder.HasIndex(x => x.IsRead);
        builder.HasIndex(x => new { x.RecipientNeighborId, x.IsRead });
    }
}
