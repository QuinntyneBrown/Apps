using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.ReviewId);

        builder.Property(r => r.ContentType)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(r => r.ReviewText)
            .IsRequired();

        builder.Property(r => r.TargetAudience)
            .HasMaxLength(500);

        builder.HasIndex(r => r.UserId);
        builder.HasIndex(r => r.ContentId);

        builder.HasIndex(r => new { r.UserId, r.ContentId, r.ContentType })
            .IsUnique();

        builder.HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(r => r.Themes)
            .WithOne(t => t.Review)
            .HasForeignKey(t => t.ReviewId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
