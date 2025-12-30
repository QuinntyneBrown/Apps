// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BBQGrillingRecipeBook.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BBQGrillingRecipeBook.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the CookSession entity.
/// </summary>
public class CookSessionConfiguration : IEntityTypeConfiguration<CookSession>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<CookSession> builder)
    {
        builder.ToTable("CookSessions");

        builder.HasKey(x => x.CookSessionId);

        builder.Property(x => x.CookSessionId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.RecipeId)
            .IsRequired();

        builder.Property(x => x.CookDate)
            .IsRequired();

        builder.Property(x => x.ActualCookTimeMinutes)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.Modifications)
            .HasMaxLength(1000);

        builder.Property(x => x.WasSuccessful)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Ignore(x => x.IsRatingValid());

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.RecipeId);
        builder.HasIndex(x => x.CookDate);
        builder.HasIndex(x => new { x.UserId, x.Rating });
    }
}
