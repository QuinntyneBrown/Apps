// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotographySessionLogger.Core;

namespace PhotographySessionLogger.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Session entity.
/// </summary>
public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable("Sessions");

        builder.HasKey(x => x.SessionId);

        builder.Property(x => x.SessionId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.SessionType)
            .IsRequired();

        builder.Property(x => x.SessionDate)
            .IsRequired();

        builder.Property(x => x.Location)
            .HasMaxLength(500);

        builder.Property(x => x.Client)
            .HasMaxLength(200);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.SessionType);
        builder.HasIndex(x => x.SessionDate);
    }
}
