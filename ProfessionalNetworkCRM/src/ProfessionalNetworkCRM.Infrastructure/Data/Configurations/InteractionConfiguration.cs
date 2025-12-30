// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfessionalNetworkCRM.Core;

namespace ProfessionalNetworkCRM.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Interaction entity.
/// </summary>
public class InteractionConfiguration : IEntityTypeConfiguration<Interaction>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Interaction> builder)
    {
        builder.ToTable("Interactions");

        builder.HasKey(x => x.InteractionId);

        builder.Property(x => x.InteractionId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ContactId)
            .IsRequired();

        builder.Property(x => x.InteractionType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.InteractionDate)
            .IsRequired();

        builder.Property(x => x.Subject)
            .HasMaxLength(500);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.Outcome)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ContactId);
        builder.HasIndex(x => x.InteractionDate);

        builder.HasOne(x => x.Contact)
            .WithMany(x => x.Interactions)
            .HasForeignKey(x => x.ContactId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
