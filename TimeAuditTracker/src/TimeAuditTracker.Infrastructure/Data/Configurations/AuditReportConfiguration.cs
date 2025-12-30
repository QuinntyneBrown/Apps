// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using TimeAuditTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeAuditTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the AuditReport entity.
/// </summary>
public class AuditReportConfiguration : IEntityTypeConfiguration<AuditReport>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<AuditReport> builder)
    {
        builder.ToTable("AuditReports");

        builder.HasKey(x => x.AuditReportId);

        builder.Property(x => x.AuditReportId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.Property(x => x.TotalTrackedHours)
            .IsRequired();

        builder.Property(x => x.ProductiveHours)
            .IsRequired();

        builder.Property(x => x.Summary)
            .HasMaxLength(2000);

        builder.Property(x => x.Insights)
            .HasMaxLength(2000);

        builder.Property(x => x.Recommendations)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.StartDate);
        builder.HasIndex(x => new { x.UserId, x.StartDate, x.EndDate });
    }
}
