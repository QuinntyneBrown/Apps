namespace MovieTVShowWatchlist.Core.Tests;

[TestFixture]
public class DomainEventTests
{
    [Test]
    public void DomainEvent_ShouldHaveUniqueEventId()
    {
        var event1 = new MovieAddedToWatchlist();
        var event2 = new MovieAddedToWatchlist();

        Assert.That(event1.EventId, Is.Not.EqualTo(event2.EventId));
    }

    [Test]
    public void DomainEvent_ShouldHaveOccurredAtTimestamp()
    {
        var beforeCreation = DateTime.UtcNow;
        var domainEvent = new MovieWatched();
        var afterCreation = DateTime.UtcNow;

        Assert.That(domainEvent.OccurredAt, Is.GreaterThanOrEqualTo(beforeCreation));
        Assert.That(domainEvent.OccurredAt, Is.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void MovieAddedToWatchlist_ShouldSetProperties()
    {
        var movieEvent = new MovieAddedToWatchlist
        {
            MovieId = Guid.NewGuid(),
            Title = "Test Movie",
            ReleaseYear = 2024,
            Genres = new List<string> { "Action", "Comedy" },
            Director = "Test Director",
            Runtime = 120,
            AddedDate = DateTime.UtcNow,
            PriorityLevel = "High",
            RecommendationSource = "Friend",
            Availability = "Netflix"
        };

        Assert.That(movieEvent.Title, Is.EqualTo("Test Movie"));
        Assert.That(movieEvent.ReleaseYear, Is.EqualTo(2024));
        Assert.That(movieEvent.Genres, Has.Count.EqualTo(2));
        Assert.That(movieEvent.Runtime, Is.EqualTo(120));
    }

    [Test]
    public void TVShowAddedToWatchlist_ShouldSetProperties()
    {
        var showEvent = new TVShowAddedToWatchlist
        {
            ShowId = Guid.NewGuid(),
            Title = "Test Show",
            PremiereYear = 2020,
            Genres = new List<string> { "Drama" },
            NumberOfSeasons = 5,
            Status = "Ongoing",
            AddedDate = DateTime.UtcNow,
            Priority = "MustWatch",
            StreamingPlatform = "HBO Max"
        };

        Assert.That(showEvent.Title, Is.EqualTo("Test Show"));
        Assert.That(showEvent.NumberOfSeasons, Is.EqualTo(5));
        Assert.That(showEvent.Status, Is.EqualTo("Ongoing"));
    }

    [Test]
    public void WatchlistItemRemoved_ShouldCalculateTimeOnWatchlist()
    {
        var removedEvent = new WatchlistItemRemoved
        {
            ItemId = Guid.NewGuid(),
            ItemType = "Movie",
            RemovalDate = DateTime.UtcNow,
            RemovalReason = "Watched",
            TimeOnWatchlist = TimeSpan.FromDays(30)
        };

        Assert.That(removedEvent.TimeOnWatchlist.Days, Is.EqualTo(30));
    }

    [Test]
    public void MovieWatched_ShouldSetViewingDetails()
    {
        var watchedEvent = new MovieWatched
        {
            MovieId = Guid.NewGuid(),
            WatchDate = DateTime.UtcNow,
            ViewingLocation = "Home",
            ViewingPlatform = "Netflix",
            WatchedWith = new List<string> { "Friend1", "Friend2" },
            ViewingContext = "Streaming",
            IsRewatch = false
        };

        Assert.That(watchedEvent.WatchedWith, Has.Count.EqualTo(2));
        Assert.That(watchedEvent.IsRewatch, Is.False);
    }

    [Test]
    public void MovieRated_ShouldSetRatingDetails()
    {
        var ratedEvent = new MovieRated
        {
            RatingId = Guid.NewGuid(),
            MovieId = Guid.NewGuid(),
            RatingValue = 8.5m,
            RatingScale = "TenPoint",
            RatingDate = DateTime.UtcNow,
            ViewingDate = DateTime.UtcNow.AddDays(-1),
            IsRewatchRating = false,
            Mood = "Happy"
        };

        Assert.That(ratedEvent.RatingValue, Is.EqualTo(8.5m));
        Assert.That(ratedEvent.RatingScale, Is.EqualTo("TenPoint"));
    }

    [Test]
    public void ReviewWritten_ShouldSetReviewDetails()
    {
        var reviewEvent = new ReviewWritten
        {
            ReviewId = Guid.NewGuid(),
            ContentId = Guid.NewGuid(),
            ContentType = "Movie",
            ReviewText = "This is a great movie!",
            HasSpoilers = false,
            ReviewDate = DateTime.UtcNow,
            ThemesDiscussed = new List<string> { "Friendship", "Adventure" },
            WouldRecommend = true,
            TargetAudience = "Adults"
        };

        Assert.That(reviewEvent.WouldRecommend, Is.True);
        Assert.That(reviewEvent.ThemesDiscussed, Has.Count.EqualTo(2));
    }

    [Test]
    public void FavoriteMarked_ShouldSetFavoriteDetails()
    {
        var favoriteEvent = new FavoriteMarked
        {
            FavoriteId = Guid.NewGuid(),
            ContentId = Guid.NewGuid(),
            ContentType = "Movie",
            AddedToFavoritesDate = DateTime.UtcNow,
            FavoriteCategory = "All-Time Best",
            RewatchCount = 5,
            EmotionalSignificance = "First movie I watched with my family"
        };

        Assert.That(favoriteEvent.RewatchCount, Is.EqualTo(5));
        Assert.That(favoriteEvent.FavoriteCategory, Is.EqualTo("All-Time Best"));
    }
}
