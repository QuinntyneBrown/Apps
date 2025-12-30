// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BBQGrillingRecipeBook.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BBQGrillingRecipeBook.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Technique entity.
/// </summary>
public class TechniqueConfiguration : IEntityTypeConfiguration<Technique>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Technique> builder)
    {
        builder.ToTable("Techniques");

        builder.HasKey(x => x.TechniqueId);

        builder.Property(x => x.TechniqueId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.Category)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.DifficultyLevel)
            .IsRequired();

        builder.Property(x => x.Instructions)
            .HasMaxLength(5000)
            .IsRequired();

        builder.Property(x => x.Tips)
            .HasMaxLength(2000);

        builder.Property(x => x.IsFavorite)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Ignore(x => x.ToggleFavorite());

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Category);
        builder.HasIndex(x => new { x.UserId, x.IsFavorite });
    }
}
