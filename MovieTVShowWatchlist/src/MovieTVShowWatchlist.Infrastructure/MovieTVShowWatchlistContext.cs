using Microsoft.EntityFrameworkCore;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Infrastructure;

public class MovieTVShowWatchlistContext : DbContext, IMovieTVShowWatchlistContext
{
    public MovieTVShowWatchlistContext(DbContextOptions<MovieTVShowWatchlistContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<TVShow> TVShows => Set<TVShow>();
    public DbSet<Episode> Episodes => Set<Episode>();
    public DbSet<ContentGenre> ContentGenres => Set<ContentGenre>();
    public DbSet<ContentAvailability> ContentAvailabilities => Set<ContentAvailability>();
    public DbSet<WatchlistItem> WatchlistItems => Set<WatchlistItem>();
    public DbSet<ViewingRecord> ViewingRecords => Set<ViewingRecord>();
    public DbSet<EpisodeViewingRecord> EpisodeViewingRecords => Set<EpisodeViewingRecord>();
    public DbSet<ViewingCompanion> ViewingCompanions => Set<ViewingCompanion>();
    public DbSet<ShowProgress> ShowProgresses => Set<ShowProgress>();
    public DbSet<AbandonedContent> AbandonedContents => Set<AbandonedContent>();
    public DbSet<BingeSession> BingeSessions => Set<BingeSession>();
    public DbSet<Rating> Ratings => Set<Rating>();
    public DbSet<SeasonRating> SeasonRatings => Set<SeasonRating>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<ReviewTheme> ReviewThemes => Set<ReviewTheme>();
    public DbSet<Favorite> Favorites => Set<Favorite>();
    public DbSet<Recommendation> Recommendations => Set<Recommendation>();
    public DbSet<SimilarContent> SimilarContents => Set<SimilarContent>();
    public DbSet<GenrePreference> GenrePreferences => Set<GenrePreference>();
    public DbSet<ViewingStreak> ViewingStreaks => Set<ViewingStreak>();
    public DbSet<ViewingMilestone> ViewingMilestones => Set<ViewingMilestone>();
    public DbSet<ViewingStatistics> ViewingStatistics => Set<ViewingStatistics>();
    public DbSet<YearInReview> YearInReviews => Set<YearInReview>();
    public DbSet<StreamingSubscription> StreamingSubscriptions => Set<StreamingSubscription>();
    public DbSet<WatchParty> WatchParties => Set<WatchParty>();
    public DbSet<WatchPartyParticipant> WatchPartyParticipants => Set<WatchPartyParticipant>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieTVShowWatchlistContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
