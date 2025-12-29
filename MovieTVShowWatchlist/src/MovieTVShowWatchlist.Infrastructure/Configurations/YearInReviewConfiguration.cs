using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class YearInReviewConfiguration : IEntityTypeConfiguration<YearInReview>
{
    public void Configure(EntityTypeBuilder<YearInReview> builder)
    {
        builder.HasKey(y => y.YearInReviewId);

        builder.Property(y => y.FavoriteGenres)
            .HasMaxLength(1000);

        builder.Property(y => y.TopRatedContent)
            .HasMaxLength(2000);

        builder.Property(y => y.ViewingTrends)
            .HasMaxLength(2000);

        builder.Property(y => y.MemorableMoments)
            .HasMaxLength(2000);

        builder.HasIndex(y => new { y.UserId, y.Year })
            .IsUnique();

        builder.HasOne(y => y.User)
            .WithMany()
            .HasForeignKey(y => y.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
