// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BookReadingTrackerLibrary.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookReadingTrackerLibrary.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Book entity.
/// </summary>
public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");

        builder.HasKey(x => x.BookId);

        builder.Property(x => x.BookId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Author)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.ISBN)
            .HasMaxLength(20);

        builder.Property(x => x.Genre)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.TotalPages)
            .IsRequired();

        builder.Property(x => x.CurrentPage)
            .IsRequired();

        builder.Property(x => x.StartDate);

        builder.Property(x => x.FinishDate);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.Genre);
    }
}
