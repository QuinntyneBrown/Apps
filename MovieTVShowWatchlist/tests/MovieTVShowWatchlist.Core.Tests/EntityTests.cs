namespace MovieTVShowWatchlist.Core.Tests;

[TestFixture]
public class EntityTests
{
    [Test]
    public void User_ShouldInitializeCollections()
    {
        var user = new User();

        Assert.That(user.WatchlistItems, Is.Not.Null);
        Assert.That(user.ViewingRecords, Is.Not.Null);
        Assert.That(user.Ratings, Is.Not.Null);
        Assert.That(user.Reviews, Is.Not.Null);
        Assert.That(user.Favorites, Is.Not.Null);
        Assert.That(user.StreamingSubscriptions, Is.Not.Null);
    }

    [Test]
    public void User_ShouldSetProperties()
    {
        var userId = Guid.NewGuid();
        var user = new User
        {
            UserId = userId,
            Username = "testuser",
            Email = "test@example.com",
            DisplayName = "Test User",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        Assert.That(user.UserId, Is.EqualTo(userId));
        Assert.That(user.Username, Is.EqualTo("testuser"));
        Assert.That(user.Email, Is.EqualTo("test@example.com"));
    }

    [Test]
    public void Movie_ShouldInitializeCollections()
    {
        var movie = new Movie();

        Assert.That(movie.Genres, Is.Not.Null);
        Assert.That(movie.Availabilities, Is.Not.Null);
    }

    [Test]
    public void Movie_ShouldSetProperties()
    {
        var movie = new Movie
        {
            MovieId = Guid.NewGuid(),
            Title = "The Matrix",
            ReleaseYear = 1999,
            Director = "The Wachowskis",
            Runtime = 136,
            ExternalId = "tt0133093"
        };

        Assert.That(movie.Title, Is.EqualTo("The Matrix"));
        Assert.That(movie.ReleaseYear, Is.EqualTo(1999));
        Assert.That(movie.Runtime, Is.EqualTo(136));
    }

    [Test]
    public void TVShow_ShouldSetStatusEnum()
    {
        var show = new TVShow
        {
            TVShowId = Guid.NewGuid(),
            Title = "Breaking Bad",
            PremiereYear = 2008,
            NumberOfSeasons = 5,
            Status = ShowStatus.Ended
        };

        Assert.That(show.Status, Is.EqualTo(ShowStatus.Ended));
    }

    [Test]
    public void Episode_ShouldSetProperties()
    {
        var showId = Guid.NewGuid();
        var episode = new Episode
        {
            EpisodeId = Guid.NewGuid(),
            TVShowId = showId,
            SeasonNumber = 1,
            EpisodeNumber = 1,
            Title = "Pilot",
            AirDate = new DateTime(2008, 1, 20),
            Runtime = 58
        };

        Assert.That(episode.SeasonNumber, Is.EqualTo(1));
        Assert.That(episode.EpisodeNumber, Is.EqualTo(1));
        Assert.That(episode.Title, Is.EqualTo("Pilot"));
    }

    [Test]
    public void WatchlistItem_ShouldSetContentType()
    {
        var item = new WatchlistItem
        {
            WatchlistItemId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            Title = "Test Movie",
            PriorityLevel = PriorityLevel.High
        };

        Assert.That(item.ContentType, Is.EqualTo(ContentType.Movie));
        Assert.That(item.PriorityLevel, Is.EqualTo(PriorityLevel.High));
    }

    [Test]
    public void WatchlistItem_ShouldTrackDeletion()
    {
        var item = new WatchlistItem
        {
            WatchlistItemId = Guid.NewGuid(),
            IsDeleted = true,
            DeletedAt = DateTime.UtcNow,
            RemovalReason = RemovalReason.Watched
        };

        Assert.That(item.IsDeleted, Is.True);
        Assert.That(item.RemovalReason, Is.EqualTo(RemovalReason.Watched));
    }

    [Test]
    public void ViewingRecord_ShouldSetViewingContext()
    {
        var record = new ViewingRecord
        {
            ViewingRecordId = Guid.NewGuid(),
            ViewingContext = ViewingContext.Theater,
            IsRewatch = false
        };

        Assert.That(record.ViewingContext, Is.EqualTo(ViewingContext.Theater));
    }

    [Test]
    public void Rating_ShouldSetRatingScale()
    {
        var rating = new Rating
        {
            RatingId = Guid.NewGuid(),
            RatingValue = 4.5m,
            RatingScale = RatingScale.FiveStar
        };

        Assert.That(rating.RatingScale, Is.EqualTo(RatingScale.FiveStar));
        Assert.That(rating.RatingValue, Is.EqualTo(4.5m));
    }

    [Test]
    public void Review_ShouldInitializeThemes()
    {
        var review = new Review();

        Assert.That(review.Themes, Is.Not.Null);
    }

    [Test]
    public void Favorite_ShouldSetCategory()
    {
        var favorite = new Favorite
        {
            FavoriteId = Guid.NewGuid(),
            FavoriteCategory = "Comfort Watch",
            RewatchCount = 10,
            EmotionalSignificance = "Childhood memory"
        };

        Assert.That(favorite.FavoriteCategory, Is.EqualTo("Comfort Watch"));
        Assert.That(favorite.RewatchCount, Is.EqualTo(10));
    }

    [Test]
    public void Recommendation_ShouldSetSource()
    {
        var recommendation = new Recommendation
        {
            RecommendationId = Guid.NewGuid(),
            Source = RecommendationSource.Friend,
            InterestLevel = InterestLevel.VeryInterested
        };

        Assert.That(recommendation.Source, Is.EqualTo(RecommendationSource.Friend));
        Assert.That(recommendation.InterestLevel, Is.EqualTo(InterestLevel.VeryInterested));
    }

    [Test]
    public void GenrePreference_ShouldSetTrendDirection()
    {
        var preference = new GenrePreference
        {
            GenrePreferenceId = Guid.NewGuid(),
            Genre = "Action",
            PreferenceStrength = 0.85m,
            TrendDirection = TrendDirection.Increasing
        };

        Assert.That(preference.TrendDirection, Is.EqualTo(TrendDirection.Increasing));
        Assert.That(preference.PreferenceStrength, Is.EqualTo(0.85m));
    }

    [Test]
    public void ViewingMilestone_ShouldSetMilestoneType()
    {
        var milestone = new ViewingMilestone
        {
            ViewingMilestoneId = Guid.NewGuid(),
            MilestoneType = MilestoneType.MoviesWatched,
            MetricAchieved = 100,
            CelebrationTier = CelebrationTier.Gold
        };

        Assert.That(milestone.MilestoneType, Is.EqualTo(MilestoneType.MoviesWatched));
        Assert.That(milestone.CelebrationTier, Is.EqualTo(CelebrationTier.Gold));
    }

    [Test]
    public void StreamingSubscription_ShouldSetStatus()
    {
        var subscription = new StreamingSubscription
        {
            StreamingSubscriptionId = Guid.NewGuid(),
            PlatformName = "Netflix",
            Status = SubscriptionStatus.Active,
            MonthlyCost = 15.99m
        };

        Assert.That(subscription.Status, Is.EqualTo(SubscriptionStatus.Active));
        Assert.That(subscription.MonthlyCost, Is.EqualTo(15.99m));
    }

    [Test]
    public void WatchParty_ShouldInitializeParticipants()
    {
        var party = new WatchParty
        {
            WatchPartyId = Guid.NewGuid(),
            Status = WatchPartyStatus.Scheduled
        };

        Assert.That(party.Participants, Is.Not.Null);
        Assert.That(party.Status, Is.EqualTo(WatchPartyStatus.Scheduled));
    }

    [Test]
    public void WatchPartyParticipant_ShouldSetStatus()
    {
        var participant = new WatchPartyParticipant
        {
            WatchPartyParticipantId = Guid.NewGuid(),
            Status = ParticipantStatus.Accepted
        };

        Assert.That(participant.Status, Is.EqualTo(ParticipantStatus.Accepted));
    }
}
