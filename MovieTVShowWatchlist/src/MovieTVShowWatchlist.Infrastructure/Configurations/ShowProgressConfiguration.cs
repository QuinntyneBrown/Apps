using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class ShowProgressConfiguration : IEntityTypeConfiguration<ShowProgress>
{
    public void Configure(EntityTypeBuilder<ShowProgress> builder)
    {
        builder.HasKey(s => s.ShowProgressId);

        builder.HasIndex(s => new { s.UserId, s.TVShowId })
            .IsUnique();

        builder.HasOne(s => s.User)
            .WithMany()
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.TVShow)
            .WithMany()
            .HasForeignKey(s => s.TVShowId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
