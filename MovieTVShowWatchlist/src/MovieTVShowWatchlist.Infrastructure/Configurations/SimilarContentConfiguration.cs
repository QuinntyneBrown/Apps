using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class SimilarContentConfiguration : IEntityTypeConfiguration<SimilarContent>
{
    public void Configure(EntityTypeBuilder<SimilarContent> builder)
    {
        builder.HasKey(s => s.SimilarContentId);

        builder.Property(s => s.SourceContentType)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(s => s.SimilarToContentType)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(s => s.SimilarityScore)
            .HasPrecision(5, 4);

        builder.Property(s => s.MatchReasons)
            .HasMaxLength(1000);

        builder.Property(s => s.AlgorithmVersion)
            .HasMaxLength(50);
    }
}
