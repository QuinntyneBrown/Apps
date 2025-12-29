using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class WatchlistItemConfiguration : IEntityTypeConfiguration<WatchlistItem>
{
    public void Configure(EntityTypeBuilder<WatchlistItem> builder)
    {
        builder.HasKey(w => w.WatchlistItemId);

        builder.Property(w => w.Title)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(w => w.ContentType)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(w => w.PriorityLevel)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(w => w.RemovalReason)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(w => w.RecommendationSource)
            .HasMaxLength(200);

        builder.Property(w => w.MoodCategory)
            .HasMaxLength(100);

        builder.HasIndex(w => w.UserId);
        builder.HasIndex(w => w.PriorityRank);

        builder.HasOne(w => w.User)
            .WithMany(u => u.WatchlistItems)
            .HasForeignKey(w => w.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(w => !w.IsDeleted);
    }
}
