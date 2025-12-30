// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PasswordAccountAuditor.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PasswordAccountAuditor.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the SecurityAudit entity.
/// </summary>
public class SecurityAuditConfiguration : IEntityTypeConfiguration<SecurityAudit>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<SecurityAudit> builder)
    {
        builder.ToTable("SecurityAudits");

        builder.HasKey(x => x.SecurityAuditId);

        builder.Property(x => x.SecurityAuditId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.AccountId)
            .IsRequired();

        builder.Property(x => x.AuditType)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.AuditDate)
            .IsRequired();

        builder.Property(x => x.Findings)
            .HasMaxLength(2000);

        builder.Property(x => x.Recommendations)
            .HasMaxLength(2000);

        builder.Property(x => x.SecurityScore)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.HasIndex(x => x.AccountId);
        builder.HasIndex(x => x.AuditType);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.AuditDate);
    }
}
