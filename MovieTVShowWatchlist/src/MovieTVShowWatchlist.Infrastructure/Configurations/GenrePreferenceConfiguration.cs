using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class GenrePreferenceConfiguration : IEntityTypeConfiguration<GenrePreference>
{
    public void Configure(EntityTypeBuilder<GenrePreference> builder)
    {
        builder.HasKey(g => g.GenrePreferenceId);

        builder.Property(g => g.Genre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(g => g.PreferenceStrength)
            .HasPrecision(5, 4);

        builder.Property(g => g.TrendDirection)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(g => g.Evidence)
            .HasMaxLength(1000);

        builder.HasOne(g => g.User)
            .WithMany()
            .HasForeignKey(g => g.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
