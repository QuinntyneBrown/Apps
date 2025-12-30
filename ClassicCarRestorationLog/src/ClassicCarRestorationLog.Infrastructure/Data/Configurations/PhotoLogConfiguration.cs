// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassicCarRestorationLog.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for the <see cref="PhotoLog"/> entity.
/// </summary>
public class PhotoLogConfiguration : IEntityTypeConfiguration<PhotoLog>
{
    /// <summary>
    /// Configures the entity of type <see cref="PhotoLog"/>.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<PhotoLog> builder)
    {
        builder.HasKey(p => p.PhotoLogId);

        builder.Property(p => p.ProjectId)
            .IsRequired();

        builder.Property(p => p.PhotoDate)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.Property(p => p.PhotoUrl)
            .HasMaxLength(1000);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.HasOne(p => p.Project)
            .WithMany(pr => pr.PhotoLogs)
            .HasForeignKey(p => p.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
