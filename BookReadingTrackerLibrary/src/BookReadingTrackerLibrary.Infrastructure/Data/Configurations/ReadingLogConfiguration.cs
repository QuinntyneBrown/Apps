// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BookReadingTrackerLibrary.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookReadingTrackerLibrary.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the ReadingLog entity.
/// </summary>
public class ReadingLogConfiguration : IEntityTypeConfiguration<ReadingLog>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<ReadingLog> builder)
    {
        builder.ToTable("ReadingLogs");

        builder.HasKey(x => x.ReadingLogId);

        builder.Property(x => x.ReadingLogId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.BookId)
            .IsRequired();

        builder.Property(x => x.StartPage)
            .IsRequired();

        builder.Property(x => x.EndPage)
            .IsRequired();

        builder.Property(x => x.StartTime)
            .IsRequired();

        builder.Property(x => x.EndTime);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.BookId);
        builder.HasIndex(x => x.StartTime);
    }
}
