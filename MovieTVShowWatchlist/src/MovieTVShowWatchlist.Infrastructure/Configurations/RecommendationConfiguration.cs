using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class RecommendationConfiguration : IEntityTypeConfiguration<Recommendation>
{
    public void Configure(EntityTypeBuilder<Recommendation> builder)
    {
        builder.HasKey(r => r.RecommendationId);

        builder.Property(r => r.ContentType)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(r => r.Source)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(r => r.InterestLevel)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(r => r.Reason)
            .HasMaxLength(1000);

        builder.Property(r => r.RecipientFeedback)
            .HasMaxLength(500);

        builder.HasOne(r => r.Recommender)
            .WithMany()
            .HasForeignKey(r => r.RecommenderId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(r => r.Recipient)
            .WithMany()
            .HasForeignKey(r => r.RecipientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
