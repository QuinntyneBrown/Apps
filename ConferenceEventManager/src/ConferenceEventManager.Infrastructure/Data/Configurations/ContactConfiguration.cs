// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConferenceEventManager.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for the <see cref="Contact"/> entity.
/// </summary>
public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    /// <summary>
    /// Configures the entity of type <see cref="Contact"/>.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(c => c.ContactId);

        builder.Property(c => c.EventId)
            .IsRequired();

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Company)
            .HasMaxLength(200);

        builder.Property(c => c.JobTitle)
            .HasMaxLength(200);

        builder.Property(c => c.Email)
            .HasMaxLength(200);

        builder.Property(c => c.LinkedInUrl)
            .HasMaxLength(500);

        builder.Property(c => c.Notes)
            .HasMaxLength(1000);

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.HasOne(c => c.Event)
            .WithMany(e => e.Contacts)
            .HasForeignKey(c => c.EventId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
