// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DateNightIdeaGenerator.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Rating entity.
/// </summary>
public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.ToTable("Ratings");

        builder.HasKey(x => x.RatingId);

        builder.Property(x => x.RatingId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.DateIdeaId);

        builder.Property(x => x.ExperienceId);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Score)
            .IsRequired();

        builder.Property(x => x.Review)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.DateIdeaId);
        builder.HasIndex(x => x.ExperienceId);
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Score);
    }
}
