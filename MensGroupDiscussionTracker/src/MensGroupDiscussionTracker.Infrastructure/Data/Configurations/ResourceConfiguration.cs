// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MensGroupDiscussionTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MensGroupDiscussionTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Resource entity.
/// </summary>
public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.ToTable("Resources");

        builder.HasKey(x => x.ResourceId);

        builder.Property(x => x.ResourceId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.GroupId)
            .IsRequired();

        builder.Property(x => x.SharedByUserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.Description);

        builder.Property(x => x.Url)
            .HasMaxLength(1000);

        builder.Property(x => x.ResourceType)
            .HasMaxLength(100);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.GroupId);
        builder.HasIndex(x => x.SharedByUserId);
    }
}
