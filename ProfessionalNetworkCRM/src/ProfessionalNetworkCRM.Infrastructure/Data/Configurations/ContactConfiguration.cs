// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfessionalNetworkCRM.Core;

namespace ProfessionalNetworkCRM.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Contact entity.
/// </summary>
public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("Contacts");

        builder.HasKey(x => x.ContactId);

        builder.Property(x => x.ContactId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.ContactType)
            .IsRequired();

        builder.Property(x => x.Company)
            .HasMaxLength(200);

        builder.Property(x => x.JobTitle)
            .HasMaxLength(200);

        builder.Property(x => x.Email)
            .HasMaxLength(200);

        builder.Property(x => x.Phone)
            .HasMaxLength(50);

        builder.Property(x => x.LinkedInUrl)
            .HasMaxLength(500);

        builder.Property(x => x.Location)
            .HasMaxLength(200);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.Tags)
            .HasConversion(
                v => string.Join("|||", v),
                v => v.Split("|||", StringSplitOptions.RemoveEmptyEntries).ToList());

        builder.Property(x => x.DateMet)
            .IsRequired();

        builder.Property(x => x.IsPriority)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Ignore(x => x.FullName);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ContactType);
        builder.HasIndex(x => x.IsPriority);
        builder.HasIndex(x => x.LastContactedDate);
    }
}
