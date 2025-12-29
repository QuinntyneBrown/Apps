using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class ContentGenreConfiguration : IEntityTypeConfiguration<ContentGenre>
{
    public void Configure(EntityTypeBuilder<ContentGenre> builder)
    {
        builder.HasKey(c => c.ContentGenreId);

        builder.Property(c => c.Genre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.ContentType)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasIndex(c => c.ContentId);
    }
}
