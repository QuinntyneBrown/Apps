using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class ViewingStreakConfiguration : IEntityTypeConfiguration<ViewingStreak>
{
    public void Configure(EntityTypeBuilder<ViewingStreak> builder)
    {
        builder.HasKey(v => v.ViewingStreakId);

        builder.HasOne(v => v.User)
            .WithMany()
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
