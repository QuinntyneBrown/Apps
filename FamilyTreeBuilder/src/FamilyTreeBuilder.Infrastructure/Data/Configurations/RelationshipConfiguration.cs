// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyTreeBuilder.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Relationship entity.
/// </summary>
public class RelationshipConfiguration : IEntityTypeConfiguration<Relationship>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Relationship> builder)
    {
        builder.ToTable("Relationships");

        builder.HasKey(x => x.RelationshipId);

        builder.Property(x => x.RelationshipId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.PersonId)
            .IsRequired();

        builder.Property(x => x.RelatedPersonId)
            .IsRequired();

        builder.Property(x => x.RelationshipType)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.PersonId);
        builder.HasIndex(x => x.RelatedPersonId);
        builder.HasIndex(x => x.RelationshipType);
    }
}
