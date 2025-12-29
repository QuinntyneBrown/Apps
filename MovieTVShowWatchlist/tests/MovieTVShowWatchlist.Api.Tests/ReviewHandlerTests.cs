namespace MovieTVShowWatchlist.Api.Tests;

[TestFixture]
public class ReviewHandlerTests
{
    private MovieTVShowWatchlistContext _context = null!;
    private Mock<ILogger<CreateReview.Handler>> _createLogger = null!;
    private Mock<ILogger<GetReviews.Handler>> _getLogger = null!;
    private User _testUser = null!;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<MovieTVShowWatchlistContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new MovieTVShowWatchlistContext(options);

        _createLogger = new Mock<ILogger<CreateReview.Handler>>();
        _getLogger = new Mock<ILogger<GetReviews.Handler>>();

        _testUser = new User
        {
            UserId = Guid.NewGuid(),
            Username = "testuser",
            Email = "test@example.com",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _context.Users.Add(_testUser);
        _context.SaveChanges();
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task CreateReview_ShouldCreateReview()
    {
        var handler = new CreateReview.Handler(_context, _createLogger.Object);
        var command = new CreateReview.Command(
            _testUser.UserId,
            Guid.NewGuid(),
            "Movie",
            "This is a great movie with amazing visuals and a compelling story!",
            false,
            new List<string> { "Adventure", "Drama" },
            true,
            "Adults"
        );

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.WouldRecommend, Is.True);
        Assert.That(result.Themes, Has.Count.EqualTo(2));
    }

    [Test]
    public void CreateReview_ShouldThrowForShortReviewText()
    {
        var handler = new CreateReview.Handler(_context, _createLogger.Object);
        var command = new CreateReview.Command(
            _testUser.UserId,
            Guid.NewGuid(),
            "Movie",
            "Too short",
            false,
            null,
            true,
            null
        );

        Assert.ThrowsAsync<ArgumentException>(async () =>
            await handler.Handle(command, CancellationToken.None));
    }

    [Test]
    public void CreateReview_ShouldThrowForInvalidContentType()
    {
        var handler = new CreateReview.Handler(_context, _createLogger.Object);
        var command = new CreateReview.Command(
            _testUser.UserId,
            Guid.NewGuid(),
            "InvalidType",
            "This is a great movie with amazing visuals and a compelling story!",
            false,
            null,
            true,
            null
        );

        Assert.ThrowsAsync<ArgumentException>(async () =>
            await handler.Handle(command, CancellationToken.None));
    }

    [Test]
    public async Task GetReviews_ShouldReturnReviews()
    {
        var review = new Review
        {
            ReviewId = Guid.NewGuid(),
            UserId = _testUser.UserId,
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            ReviewText = "Great movie!",
            HasSpoilers = false,
            ReviewDate = DateTime.UtcNow,
            WouldRecommend = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        var handler = new GetReviews.Handler(_context, _getLogger.Object);
        var query = new GetReviews.Query(_testUser.UserId, null, null, 1, 20);

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.That(result.Items, Has.Count.EqualTo(1));
    }

    [Test]
    public async Task GetReviews_ShouldFilterBySpoilers()
    {
        var spoilerReview = new Review
        {
            ReviewId = Guid.NewGuid(),
            UserId = _testUser.UserId,
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            ReviewText = "Spoiler review!",
            HasSpoilers = true,
            ReviewDate = DateTime.UtcNow,
            WouldRecommend = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var noSpoilerReview = new Review
        {
            ReviewId = Guid.NewGuid(),
            UserId = _testUser.UserId,
            ContentId = Guid.NewGuid(),
            ContentType = ContentType.Movie,
            ReviewText = "No spoiler review!",
            HasSpoilers = false,
            ReviewDate = DateTime.UtcNow,
            WouldRecommend = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Reviews.AddRange(spoilerReview, noSpoilerReview);
        await _context.SaveChangesAsync();

        var handler = new GetReviews.Handler(_context, _getLogger.Object);
        var query = new GetReviews.Query(_testUser.UserId, null, false, 1, 20);

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.That(result.Items, Has.Count.EqualTo(1));
        Assert.That(result.Items[0].HasSpoilers, Is.False);
    }
}
