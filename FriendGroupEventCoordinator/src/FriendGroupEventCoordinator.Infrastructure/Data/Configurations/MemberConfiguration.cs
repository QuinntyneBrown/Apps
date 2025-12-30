// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FriendGroupEventCoordinator.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Member entity.
/// </summary>
public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Members");

        builder.HasKey(x => x.MemberId);

        builder.Property(x => x.MemberId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.GroupId)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(200);

        builder.Property(x => x.IsAdmin)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.JoinedAt)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.RSVPs)
            .WithOne(x => x.Member)
            .HasForeignKey(x => x.MemberId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.GroupId);
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => new { x.GroupId, x.UserId })
            .IsUnique();
    }
}
