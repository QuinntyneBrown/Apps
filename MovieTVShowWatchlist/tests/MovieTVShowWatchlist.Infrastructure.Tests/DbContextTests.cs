namespace MovieTVShowWatchlist.Infrastructure.Tests;

[TestFixture]
public class DbContextTests
{
    private MovieTVShowWatchlistContext _context = null!;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<MovieTVShowWatchlistContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new MovieTVShowWatchlistContext(options);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public void Context_ShouldImplementInterface()
    {
        Assert.That(_context, Is.InstanceOf<IMovieTVShowWatchlistContext>());
    }

    [Test]
    public async Task Context_ShouldSaveAndRetrieveUser()
    {
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Username = "testuser",
            Email = "test@example.com",
            DisplayName = "Test User",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Users.FindAsync(user.UserId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Username, Is.EqualTo("testuser"));
    }

    [Test]
    public async Task Context_ShouldSaveAndRetrieveMovie()
    {
        var movie = new Movie
        {
            MovieId = Guid.NewGuid(),
            Title = "Test Movie",
            ReleaseYear = 2024,
            Director = "Test Director",
            Runtime = 120,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Movies.FindAsync(movie.MovieId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Movie"));
    }

    [Test]
    public async Task Context_ShouldSaveAndRetrieveTVShow()
    {
        var show = new TVShow
        {
            TVShowId = Guid.NewGuid(),
            Title = "Test Show",
            PremiereYear = 2020,
            NumberOfSeasons = 5,
            Status = ShowStatus.Ongoing,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.TVShows.Add(show);
        await _context.SaveChangesAsync();

        var retrieved = await _context.TVShows.FindAsync(show.TVShowId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Status, Is.EqualTo(ShowStatus.Ongoing));
    }

    [Test]
    public async Task Context_ShouldSaveWatchlistItem()
    {
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Username = "testuser",
            Email = "test@example.com",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);

        var item = new WatchlistItem
        {
            WatchlistItemId = Guid.NewGuid(),
            UserId = user.UserId,
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            Title = "Test Movie",
            AddedDate = DateTime.UtcNow,
            PriorityLevel = PriorityLevel.High,
            PriorityRank = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.WatchlistItems.Add(item);
        await _context.SaveChangesAsync();

        var retrieved = await _context.WatchlistItems.FindAsync(item.WatchlistItemId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.PriorityLevel, Is.EqualTo(PriorityLevel.High));
    }

    [Test]
    public async Task Context_ShouldFilterDeletedWatchlistItems()
    {
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Username = "testuser",
            Email = "test@example.com",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);

        var activeItem = new WatchlistItem
        {
            WatchlistItemId = Guid.NewGuid(),
            UserId = user.UserId,
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            Title = "Active Movie",
            AddedDate = DateTime.UtcNow,
            IsDeleted = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var deletedItem = new WatchlistItem
        {
            WatchlistItemId = Guid.NewGuid(),
            UserId = user.UserId,
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            Title = "Deleted Movie",
            AddedDate = DateTime.UtcNow,
            IsDeleted = true,
            DeletedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.WatchlistItems.AddRange(activeItem, deletedItem);
        await _context.SaveChangesAsync();

        var items = await _context.WatchlistItems.Where(w => w.UserId == user.UserId).ToListAsync();
        Assert.That(items, Has.Count.EqualTo(1));
        Assert.That(items[0].Title, Is.EqualTo("Active Movie"));
    }

    [Test]
    public async Task Context_ShouldSaveRating()
    {
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Username = "testuser",
            Email = "test@example.com",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);

        var rating = new Rating
        {
            RatingId = Guid.NewGuid(),
            UserId = user.UserId,
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            RatingValue = 8.5m,
            RatingScale = RatingScale.TenPoint,
            RatingDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Ratings.Add(rating);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Ratings.FindAsync(rating.RatingId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.RatingValue, Is.EqualTo(8.5m));
    }

    [Test]
    public async Task Context_ShouldSaveReviewWithThemes()
    {
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Username = "testuser",
            Email = "test@example.com",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);

        var review = new Review
        {
            ReviewId = Guid.NewGuid(),
            UserId = user.UserId,
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            ReviewText = "This is a great movie with amazing visuals!",
            HasSpoilers = false,
            ReviewDate = DateTime.UtcNow,
            WouldRecommend = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        review.Themes.Add(new ReviewTheme
        {
            ReviewThemeId = Guid.NewGuid(),
            Theme = "Adventure",
            CreatedAt = DateTime.UtcNow
        });

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Reviews
            .Include(r => r.Themes)
            .FirstOrDefaultAsync(r => r.ReviewId == review.ReviewId);

        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Themes, Has.Count.EqualTo(1));
    }

    [Test]
    public async Task Context_ShouldSaveFavorite()
    {
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Username = "testuser",
            Email = "test@example.com",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);

        var favorite = new Favorite
        {
            FavoriteId = Guid.NewGuid(),
            UserId = user.UserId,
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            AddedDate = DateTime.UtcNow,
            FavoriteCategory = "All-Time Best",
            RewatchCount = 5,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Favorites.Add(favorite);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Favorites.FindAsync(favorite.FavoriteId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.FavoriteCategory, Is.EqualTo("All-Time Best"));
    }

    [Test]
    public async Task Context_ShouldSaveViewingRecord()
    {
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Username = "testuser",
            Email = "test@example.com",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);

        var record = new ViewingRecord
        {
            ViewingRecordId = Guid.NewGuid(),
            UserId = user.UserId,
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            WatchDate = DateTime.UtcNow,
            ViewingPlatform = "Netflix",
            ViewingContext = ViewingContext.Streaming,
            CreatedAt = DateTime.UtcNow
        };

        _context.ViewingRecords.Add(record);
        await _context.SaveChangesAsync();

        var retrieved = await _context.ViewingRecords.FindAsync(record.ViewingRecordId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.ViewingPlatform, Is.EqualTo("Netflix"));
    }

    [Test]
    public async Task Context_ShouldSaveShowProgress()
    {
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Username = "testuser",
            Email = "test@example.com",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var show = new TVShow
        {
            TVShowId = Guid.NewGuid(),
            Title = "Test Show",
            PremiereYear = 2020,
            Status = ShowStatus.Ongoing,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        _context.TVShows.Add(show);

        var progress = new ShowProgress
        {
            ShowProgressId = Guid.NewGuid(),
            UserId = user.UserId,
            TVShowId = show.TVShowId,
            LastWatchedSeason = 3,
            LastWatchedEpisode = 5,
            TotalEpisodesWatched = 25,
            CompletedSeasons = 2,
            UpdatedAt = DateTime.UtcNow
        };

        _context.ShowProgresses.Add(progress);
        await _context.SaveChangesAsync();

        var retrieved = await _context.ShowProgresses.FindAsync(progress.ShowProgressId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.TotalEpisodesWatched, Is.EqualTo(25));
    }
}
