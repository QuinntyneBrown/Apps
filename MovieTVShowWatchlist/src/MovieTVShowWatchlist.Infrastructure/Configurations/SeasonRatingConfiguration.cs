using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class SeasonRatingConfiguration : IEntityTypeConfiguration<SeasonRating>
{
    public void Configure(EntityTypeBuilder<SeasonRating> builder)
    {
        builder.HasKey(s => s.SeasonRatingId);

        builder.Property(s => s.RatingValue)
            .HasPrecision(3, 1);
    }
}
