// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MovieTVShowWatchlist.Core;
using Microsoft.EntityFrameworkCore;

namespace MovieTVShowWatchlist.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the MovieTVShowWatchlist system.
/// </summary>
public class MovieTVShowWatchlistContext : DbContext, IMovieTVShowWatchlistContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MovieTVShowWatchlistContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public MovieTVShowWatchlistContext(DbContextOptions<MovieTVShowWatchlistContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<User> Users { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Movie> Movies { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<TVShow> TVShows { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Episode> Episodes { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ContentGenre> ContentGenres { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ContentAvailability> ContentAvailabilities { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<WatchlistItem> WatchlistItems { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ViewingRecord> ViewingRecords { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<EpisodeViewingRecord> EpisodeViewingRecords { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ViewingCompanion> ViewingCompanions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ShowProgress> ShowProgresses { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<AbandonedContent> AbandonedContents { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<BingeSession> BingeSessions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Rating> Ratings { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<SeasonRating> SeasonRatings { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Review> Reviews { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ReviewTheme> ReviewThemes { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Favorite> Favorites { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Recommendation> Recommendations { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<SimilarContent> SimilarContents { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<GenrePreference> GenrePreferences { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ViewingStreak> ViewingStreaks { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ViewingMilestone> ViewingMilestones { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ViewingStatistics> ViewingStatistics { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<YearInReview> YearInReviews { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<StreamingSubscription> StreamingSubscriptions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<WatchParty> WatchParties { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<WatchPartyParticipant> WatchPartyParticipants { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieTVShowWatchlistContext).Assembly);
    }
}
