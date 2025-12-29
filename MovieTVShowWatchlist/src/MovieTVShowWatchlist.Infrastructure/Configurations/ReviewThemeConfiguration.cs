using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class ReviewThemeConfiguration : IEntityTypeConfiguration<ReviewTheme>
{
    public void Configure(EntityTypeBuilder<ReviewTheme> builder)
    {
        builder.HasKey(r => r.ReviewThemeId);

        builder.Property(r => r.Theme)
            .IsRequired()
            .HasMaxLength(200);
    }
}
