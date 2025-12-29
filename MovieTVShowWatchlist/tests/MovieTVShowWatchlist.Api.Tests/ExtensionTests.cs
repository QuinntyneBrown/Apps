namespace MovieTVShowWatchlist.Api.Tests;

[TestFixture]
public class ExtensionTests
{
    [Test]
    public void WatchlistItemToDto_ShouldMapCorrectly()
    {
        var item = new WatchlistItem
        {
            WatchlistItemId = Guid.NewGuid(),
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            Title = "Test Movie",
            AddedDate = DateTime.UtcNow,
            PriorityLevel = PriorityLevel.High,
            PriorityRank = 1,
            RecommendationSource = "Friend",
            MoodCategory = "Action",
            WatchOrderPreference = 1
        };

        var dto = item.ToDto();

        Assert.That(dto.WatchlistItemId, Is.EqualTo(item.WatchlistItemId));
        Assert.That(dto.ContentId, Is.EqualTo(item.ContentId));
        Assert.That(dto.ContentType, Is.EqualTo("Movie"));
        Assert.That(dto.Title, Is.EqualTo("Test Movie"));
        Assert.That(dto.PriorityLevel, Is.EqualTo("High"));
    }

    [Test]
    public void ViewingRecordToDto_ShouldMapCorrectly()
    {
        var record = new ViewingRecord
        {
            ViewingRecordId = Guid.NewGuid(),
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            WatchDate = DateTime.UtcNow,
            ViewingPlatform = "Netflix",
            ViewingLocation = "Home",
            ViewingContext = ViewingContext.Streaming,
            IsRewatch = false,
            ViewingDurationMinutes = 120
        };

        var dto = record.ToDto();

        Assert.That(dto.ViewingRecordId, Is.EqualTo(record.ViewingRecordId));
        Assert.That(dto.ContentType, Is.EqualTo("Movie"));
        Assert.That(dto.ViewingPlatform, Is.EqualTo("Netflix"));
        Assert.That(dto.ViewingContext, Is.EqualTo("Streaming"));
    }

    [Test]
    public void ShowProgressToDto_ShouldMapCorrectly()
    {
        var progress = new ShowProgress
        {
            TVShowId = Guid.NewGuid(),
            LastWatchedSeason = 3,
            LastWatchedEpisode = 5,
            TotalEpisodesWatched = 25,
            CompletedSeasons = 2,
            IsCompleted = false,
            CompletionDate = null
        };

        var dto = progress.ToDto();

        Assert.That(dto.TVShowId, Is.EqualTo(progress.TVShowId));
        Assert.That(dto.LastWatchedSeason, Is.EqualTo(3));
        Assert.That(dto.TotalEpisodesWatched, Is.EqualTo(25));
    }

    [Test]
    public void RatingToDto_ShouldMapCorrectly()
    {
        var rating = new Rating
        {
            RatingId = Guid.NewGuid(),
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            RatingValue = 8.5m,
            RatingScale = RatingScale.TenPoint,
            RatingDate = DateTime.UtcNow,
            ViewingDate = DateTime.UtcNow.AddDays(-1),
            IsRewatchRating = false,
            Mood = "Happy"
        };

        var dto = rating.ToDto();

        Assert.That(dto.RatingId, Is.EqualTo(rating.RatingId));
        Assert.That(dto.RatingValue, Is.EqualTo(8.5m));
        Assert.That(dto.RatingScale, Is.EqualTo("TenPoint"));
        Assert.That(dto.Mood, Is.EqualTo("Happy"));
    }

    [Test]
    public void ReviewToDto_ShouldMapCorrectly()
    {
        var review = new Review
        {
            ReviewId = Guid.NewGuid(),
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            ReviewText = "Great movie!",
            HasSpoilers = false,
            ReviewDate = DateTime.UtcNow,
            WouldRecommend = true,
            TargetAudience = "Adults"
        };

        review.Themes.Add(new ReviewTheme { Theme = "Adventure" });
        review.Themes.Add(new ReviewTheme { Theme = "Drama" });

        var dto = review.ToDto();

        Assert.That(dto.ReviewId, Is.EqualTo(review.ReviewId));
        Assert.That(dto.ReviewText, Is.EqualTo("Great movie!"));
        Assert.That(dto.Themes, Has.Count.EqualTo(2));
    }

    [Test]
    public void FavoriteToDto_ShouldMapCorrectly()
    {
        var favorite = new Favorite
        {
            FavoriteId = Guid.NewGuid(),
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.TVShow,
            AddedDate = DateTime.UtcNow,
            FavoriteCategory = "All-Time Best",
            RewatchCount = 5,
            EmotionalSignificance = "Childhood memory"
        };

        var dto = favorite.ToDto();

        Assert.That(dto.FavoriteId, Is.EqualTo(favorite.FavoriteId));
        Assert.That(dto.ContentType, Is.EqualTo("TVShow"));
        Assert.That(dto.FavoriteCategory, Is.EqualTo("All-Time Best"));
        Assert.That(dto.RewatchCount, Is.EqualTo(5));
    }
}
