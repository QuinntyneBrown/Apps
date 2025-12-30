// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConversationStarterApp.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for the <see cref="Prompt"/> entity.
/// </summary>
public class PromptConfiguration : IEntityTypeConfiguration<Prompt>
{
    /// <summary>
    /// Configures the entity of type <see cref="Prompt"/>.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<Prompt> builder)
    {
        builder.HasKey(p => p.PromptId);

        builder.Property(p => p.Text)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(p => p.Category)
            .IsRequired();

        builder.Property(p => p.Depth)
            .IsRequired();

        builder.Property(p => p.Tags)
            .HasMaxLength(500);

        builder.Property(p => p.IsSystemPrompt)
            .IsRequired();

        builder.Property(p => p.UsageCount)
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.HasMany(p => p.Favorites)
            .WithOne(f => f.Prompt)
            .HasForeignKey(f => f.PromptId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
