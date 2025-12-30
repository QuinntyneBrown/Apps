// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MarriageEnrichmentJournal.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarriageEnrichmentJournal.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Reflection entity.
/// </summary>
public class ReflectionConfiguration : IEntityTypeConfiguration<Reflection>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Reflection> builder)
    {
        builder.ToTable("Reflections");

        builder.HasKey(x => x.ReflectionId);

        builder.Property(x => x.ReflectionId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Text)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(x => x.Topic)
            .HasMaxLength(200);

        builder.Property(x => x.ReflectionDate)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ReflectionDate);
        builder.HasIndex(x => x.JournalEntryId);
        builder.HasIndex(x => new { x.UserId, x.Topic });
    }
}
