// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfessionalReadingList.Core;

namespace ProfessionalReadingList.Infrastructure;

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

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.ResourceType)
            .IsRequired();

        builder.Property(x => x.Author)
            .HasMaxLength(300);

        builder.Property(x => x.Publisher)
            .HasMaxLength(200);

        builder.Property(x => x.Url)
            .HasMaxLength(1000);

        builder.Property(x => x.Isbn)
            .HasMaxLength(50);

        builder.Property(x => x.Topics)
            .HasConversion(
                v => string.Join("|||", v),
                v => v.Split("|||", StringSplitOptions.RemoveEmptyEntries).ToList());

        builder.Property(x => x.DateAdded)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ResourceType);
        builder.HasIndex(x => x.DateAdded);

        builder.HasOne(x => x.ReadingProgress)
            .WithOne(x => x.Resource)
            .HasForeignKey<ReadingProgress>(x => x.ResourceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
