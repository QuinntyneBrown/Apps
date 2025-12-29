using Microsoft.EntityFrameworkCore;

namespace MovieTVShowWatchlist.Core;

public interface IMovieTVShowWatchlistContext
{
    DbSet<User> Users { get; }
    DbSet<Movie> Movies { get; }
    DbSet<TVShow> TVShows { get; }
    DbSet<Episode> Episodes { get; }
    DbSet<ContentGenre> ContentGenres { get; }
    DbSet<ContentAvailability> ContentAvailabilities { get; }
    DbSet<WatchlistItem> WatchlistItems { get; }
    DbSet<ViewingRecord> ViewingRecords { get; }
    DbSet<EpisodeViewingRecord> EpisodeViewingRecords { get; }
    DbSet<ViewingCompanion> ViewingCompanions { get; }
    DbSet<ShowProgress> ShowProgresses { get; }
    DbSet<AbandonedContent> AbandonedContents { get; }
    DbSet<BingeSession> BingeSessions { get; }
    DbSet<Rating> Ratings { get; }
    DbSet<SeasonRating> SeasonRatings { get; }
    DbSet<Review> Reviews { get; }
    DbSet<ReviewTheme> ReviewThemes { get; }
    DbSet<Favorite> Favorites { get; }
    DbSet<Recommendation> Recommendations { get; }
    DbSet<SimilarContent> SimilarContents { get; }
    DbSet<GenrePreference> GenrePreferences { get; }
    DbSet<ViewingStreak> ViewingStreaks { get; }
    DbSet<ViewingMilestone> ViewingMilestones { get; }
    DbSet<ViewingStatistics> ViewingStatistics { get; }
    DbSet<YearInReview> YearInReviews { get; }
    DbSet<StreamingSubscription> StreamingSubscriptions { get; }
    DbSet<WatchParty> WatchParties { get; }
    DbSet<WatchPartyParticipant> WatchPartyParticipants { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
