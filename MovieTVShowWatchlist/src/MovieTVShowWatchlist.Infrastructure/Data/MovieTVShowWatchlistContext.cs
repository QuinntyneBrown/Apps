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
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="MovieTVShowWatchlistContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public MovieTVShowWatchlistContext(DbContextOptions<MovieTVShowWatchlistContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
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

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<User>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Movie>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<TVShow>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Episode>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<ContentGenre>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<ContentAvailability>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<WatchlistItem>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<ViewingRecord>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<EpisodeViewingRecord>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<ViewingCompanion>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<ShowProgress>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<AbandonedContent>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<BingeSession>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Rating>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<SeasonRating>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Review>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<ReviewTheme>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Favorite>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Recommendation>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<SimilarContent>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<GenrePreference>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<ViewingStreak>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<ViewingMilestone>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<ViewingStatistics>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<YearInReview>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<StreamingSubscription>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<WatchParty>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<WatchPartyParticipant>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieTVShowWatchlistContext).Assembly);
    }
}
