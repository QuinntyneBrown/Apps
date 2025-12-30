// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CampingTripPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampingTripPlanner.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Review entity.
/// </summary>
public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews");

        builder.HasKey(x => x.ReviewId);

        builder.Property(x => x.ReviewId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.CampsiteId)
            .IsRequired();

        builder.Property(x => x.Rating)
            .IsRequired();

        builder.Property(x => x.ReviewText)
            .HasMaxLength(5000);

        builder.Property(x => x.ReviewDate)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.CampsiteId);
        builder.HasIndex(x => x.Rating);
        builder.HasIndex(x => x.ReviewDate);
    }
}
