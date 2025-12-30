// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SkillDevelopmentTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SkillDevelopmentTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Certification entity.
/// </summary>
public class CertificationConfiguration : IEntityTypeConfiguration<Certification>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Certification> builder)
    {
        builder.ToTable("Certifications");

        builder.HasKey(x => x.CertificationId);

        builder.Property(x => x.CertificationId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.IssuingOrganization)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.IssueDate)
            .IsRequired();

        builder.Property(x => x.ExpirationDate);

        builder.Property(x => x.CredentialId)
            .HasMaxLength(100);

        builder.Property(x => x.CredentialUrl)
            .HasMaxLength(500);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.SkillIds)
            .HasConversion(
                v => string.Join(",", v.Select(g => g.ToString())),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                      .Select(s => Guid.Parse(s))
                      .ToList());

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.IssuingOrganization);
        builder.HasIndex(x => x.IsActive);
        builder.HasIndex(x => x.ExpirationDate);
    }
}
