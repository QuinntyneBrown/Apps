// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DateNightIdeaGenerator.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the DateIdea entity.
/// </summary>
public class DateIdeaConfiguration : IEntityTypeConfiguration<DateIdea>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<DateIdea> builder)
    {
        builder.ToTable("DateIdeas");

        builder.HasKey(x => x.DateIdeaId);

        builder.Property(x => x.DateIdeaId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.Category)
            .IsRequired();

        builder.Property(x => x.BudgetRange)
            .IsRequired();

        builder.Property(x => x.Location)
            .HasMaxLength(500);

        builder.Property(x => x.DurationMinutes);

        builder.Property(x => x.Season)
            .HasMaxLength(100);

        builder.Property(x => x.IsFavorite)
            .IsRequired();

        builder.Property(x => x.HasBeenTried)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasMany(x => x.Experiences)
            .WithOne(e => e.DateIdea)
            .HasForeignKey(e => e.DateIdeaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Ratings)
            .WithOne(r => r.DateIdea)
            .HasForeignKey(r => r.DateIdeaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Category);
        builder.HasIndex(x => x.BudgetRange);
        builder.HasIndex(x => x.IsFavorite);
        builder.HasIndex(x => x.HasBeenTried);
    }
}
