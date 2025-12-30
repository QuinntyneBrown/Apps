// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DailyJournalingApp.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyJournalingApp.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Prompt entity.
/// </summary>
public class PromptConfiguration : IEntityTypeConfiguration<Prompt>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Prompt> builder)
    {
        builder.ToTable("Prompts");

        builder.HasKey(x => x.PromptId);

        builder.Property(x => x.PromptId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Text)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.Category)
            .HasMaxLength(100);

        builder.Property(x => x.IsSystemPrompt)
            .IsRequired();

        builder.Property(x => x.CreatedByUserId);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.Category);
        builder.HasIndex(x => x.IsSystemPrompt);
        builder.HasIndex(x => x.CreatedByUserId);
    }
}
