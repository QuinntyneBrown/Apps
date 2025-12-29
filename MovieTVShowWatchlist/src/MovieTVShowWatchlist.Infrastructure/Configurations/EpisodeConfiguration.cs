using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class EpisodeConfiguration : IEntityTypeConfiguration<Episode>
{
    public void Configure(EntityTypeBuilder<Episode> builder)
    {
        builder.HasKey(e => e.EpisodeId);

        builder.Property(e => e.Title)
            .HasMaxLength(300);

        builder.HasIndex(e => new { e.TVShowId, e.SeasonNumber, e.EpisodeNumber })
            .IsUnique();
    }
}
