// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using JobSearchOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobSearchOrganizer.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Company entity.
/// </summary>
public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");

        builder.HasKey(x => x.CompanyId);

        builder.Property(x => x.CompanyId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.Industry)
            .HasMaxLength(200);

        builder.Property(x => x.Website)
            .HasMaxLength(500);

        builder.Property(x => x.Location)
            .HasMaxLength(200);

        builder.Property(x => x.CompanySize)
            .HasMaxLength(100);

        builder.Property(x => x.CultureNotes)
            .HasMaxLength(2000);

        builder.Property(x => x.ResearchNotes)
            .HasMaxLength(2000);

        builder.Property(x => x.IsTargetCompany)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.IsTargetCompany);
    }
}
