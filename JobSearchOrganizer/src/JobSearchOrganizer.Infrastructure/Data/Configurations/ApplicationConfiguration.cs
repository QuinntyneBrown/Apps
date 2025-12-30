// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using JobSearchOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobSearchOrganizer.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Application entity.
/// </summary>
public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.ToTable("Applications");

        builder.HasKey(x => x.ApplicationId);

        builder.Property(x => x.ApplicationId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.CompanyId)
            .IsRequired();

        builder.Property(x => x.JobTitle)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.JobUrl)
            .HasMaxLength(500);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.AppliedDate)
            .IsRequired();

        builder.Property(x => x.SalaryRange)
            .HasMaxLength(100);

        builder.Property(x => x.Location)
            .HasMaxLength(200);

        builder.Property(x => x.JobType)
            .HasMaxLength(100);

        builder.Property(x => x.IsRemote)
            .IsRequired();

        builder.Property(x => x.ContactPerson)
            .HasMaxLength(200);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.CompanyId);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.AppliedDate);
    }
}
