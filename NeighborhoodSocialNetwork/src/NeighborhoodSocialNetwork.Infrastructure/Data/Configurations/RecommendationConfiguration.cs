// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NeighborhoodSocialNetwork.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NeighborhoodSocialNetwork.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Recommendation entity.
/// </summary>
public class RecommendationConfiguration : IEntityTypeConfiguration<Recommendation>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Recommendation> builder)
    {
        builder.ToTable("Recommendations");

        builder.HasKey(x => x.RecommendationId);

        builder.Property(x => x.RecommendationId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.NeighborId)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(x => x.RecommendationType)
            .IsRequired();

        builder.Property(x => x.BusinessName)
            .HasMaxLength(300);

        builder.Property(x => x.Location)
            .HasMaxLength(500);

        builder.Property(x => x.Rating);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasOne(x => x.Neighbor)
            .WithMany(x => x.Recommendations)
            .HasForeignKey(x => x.NeighborId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.NeighborId);
        builder.HasIndex(x => x.RecommendationType);
        builder.HasIndex(x => x.Rating);
    }
}
