using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class EpisodeViewingRecordConfiguration : IEntityTypeConfiguration<EpisodeViewingRecord>
{
    public void Configure(EntityTypeBuilder<EpisodeViewingRecord> builder)
    {
        builder.HasKey(e => e.EpisodeViewingRecordId);

        builder.Property(e => e.Platform)
            .HasMaxLength(100);

        builder.HasIndex(e => new { e.UserId, e.TVShowId });
    }
}
