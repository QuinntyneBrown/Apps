using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class TVShowConfiguration : IEntityTypeConfiguration<TVShow>
{
    public void Configure(EntityTypeBuilder<TVShow> builder)
    {
        builder.HasKey(t => t.TVShowId);

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(t => t.ExternalId)
            .HasMaxLength(50);

        builder.Property(t => t.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasIndex(t => t.Title);
        builder.HasIndex(t => t.ExternalId);

        builder.HasMany(t => t.Episodes)
            .WithOne(e => e.TVShow)
            .HasForeignKey(e => e.TVShowId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
