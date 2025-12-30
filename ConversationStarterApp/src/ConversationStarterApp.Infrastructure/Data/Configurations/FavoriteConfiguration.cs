// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConversationStarterApp.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for the <see cref="Favorite"/> entity.
/// </summary>
public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
{
    /// <summary>
    /// Configures the entity of type <see cref="Favorite"/>.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<Favorite> builder)
    {
        builder.HasKey(f => f.FavoriteId);

        builder.Property(f => f.PromptId)
            .IsRequired();

        builder.Property(f => f.UserId)
            .IsRequired();

        builder.Property(f => f.Notes)
            .HasMaxLength(500);

        builder.Property(f => f.CreatedAt)
            .IsRequired();

        builder.HasOne(f => f.Prompt)
            .WithMany(p => p.Favorites)
            .HasForeignKey(f => f.PromptId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
