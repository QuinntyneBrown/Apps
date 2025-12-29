using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasKey(m => m.MovieId);

        builder.Property(m => m.Title)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(m => m.Director)
            .HasMaxLength(200);

        builder.Property(m => m.ExternalId)
            .HasMaxLength(50);

        builder.HasIndex(m => m.Title);
        builder.HasIndex(m => m.ExternalId);
    }
}
