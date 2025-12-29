using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
{
    public void Configure(EntityTypeBuilder<Favorite> builder)
    {
        builder.HasKey(f => f.FavoriteId);

        builder.Property(f => f.ContentType)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(f => f.FavoriteCategory)
            .HasMaxLength(100);

        builder.Property(f => f.EmotionalSignificance)
            .HasMaxLength(1000);

        builder.HasIndex(f => f.UserId);
        builder.HasIndex(f => f.FavoriteCategory);

        builder.HasIndex(f => new { f.UserId, f.ContentId, f.ContentType })
            .IsUnique();

        builder.HasOne(f => f.User)
            .WithMany(u => u.Favorites)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
