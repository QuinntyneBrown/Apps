// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DateNightIdeaGenerator.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Experience entity.
/// </summary>
public class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Experience> builder)
    {
        builder.ToTable("Experiences");

        builder.HasKey(x => x.ExperienceId);

        builder.Property(x => x.ExperienceId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.DateIdeaId)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ExperienceDate)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(5000);

        builder.Property(x => x.ActualCost)
            .HasPrecision(10, 2);

        builder.Property(x => x.Photos)
            .HasMaxLength(2000);

        builder.Property(x => x.WasSuccessful)
            .IsRequired();

        builder.Property(x => x.WouldRepeat)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.Ratings)
            .WithOne(r => r.Experience)
            .HasForeignKey(r => r.ExperienceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.DateIdeaId);
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ExperienceDate);
        builder.HasIndex(x => x.WasSuccessful);
    }
}
