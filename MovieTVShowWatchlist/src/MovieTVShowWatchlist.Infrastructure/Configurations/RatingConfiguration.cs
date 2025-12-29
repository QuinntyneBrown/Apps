using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.HasKey(r => r.RatingId);

        builder.Property(r => r.ContentType)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(r => r.RatingValue)
            .HasPrecision(3, 1);

        builder.Property(r => r.RatingScale)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(r => r.Mood)
            .HasMaxLength(100);

        builder.HasIndex(r => r.UserId);
        builder.HasIndex(r => r.ContentId);
        builder.HasIndex(r => r.RatingValue);

        builder.HasOne(r => r.User)
            .WithMany(u => u.Ratings)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(r => r.SeasonRatings)
            .WithOne(s => s.Rating)
            .HasForeignKey(s => s.RatingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
